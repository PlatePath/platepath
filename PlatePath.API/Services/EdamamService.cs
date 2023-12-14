using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Data;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Singleton;
using System;
using System.Dynamic;
using System.Net.Http;

namespace PlatePath.API.Services;

public class EdamamService : IEdamamService
{
    readonly IEdamamClient _edamamClient;
    readonly ILogger<EdamamService> _logger;
    readonly Configuration _cfg;
    readonly IProfileService _profileService;
    readonly ApplicationDbContext _dbContext;
    readonly IBlobStorageService _blobStorageService;

    public EdamamService(IEdamamClient edamamClient, IProfileService profileService, IBlobStorageService blobStorageService, ApplicationDbContext dbContext, ILogger<EdamamService> logger, IOptions<Configuration> cfg)
    {
        _edamamClient = edamamClient;
        _profileService = profileService;
        _blobStorageService = blobStorageService;
        _dbContext = dbContext;
        _logger = logger;
        _cfg = cfg.Value;
    }

    public async Task<GenerateMealPlanResponse> GenerateMealPlan(string userId, GenerateMealPlanRequest request) //TODO add params
    {
        if (request.MaxCalories is null || request.MinCalories is null || request.Proteins is null ||
            request.Carbohydrates is null || request.Fats is null)
        {
            var nutritionsResponse = _profileService.CalculateNutrition(userId);
            if (nutritionsResponse is not null)
            {
                request.MinCalories = nutritionsResponse.Calories - 100;
                request.MaxCalories = nutritionsResponse.Calories + 100;
                request.Proteins = nutritionsResponse.ProteinGrams;
                request.Carbohydrates = nutritionsResponse.CarbGrams;
                request.Fats = nutritionsResponse.FatGrams;
            }
        }

        var proteins = request.Proteins ??= 130;
        var carbohydrates = request.Carbohydrates ??= 250;

        var mealplanRequest = new EdamamMealPlanRequest
        {
            size = request.Days,
            plan = new Plan
            {
                accept = new Accept
                {
                    all = new List<All>
                        {
                           new All
                           {
                               health = new List<string>
                               {
                                   request.DietType
                               }
                           }
                        }
                },
                fit = new Fit
                {
                    ENERC_KCAL = new MinMaxQuantity
                    {
                        min = request.MinCalories ??= 1000,
                        max = request.MaxCalories ??= 5000
                    }
                    ,
                    PROCNT = new MinMaxQuantity
                    {
                        min = proteins - 30,
                        max = proteins + 30
                    },
                    CHOCDF = new MinMaxQuantity
                    {
                        min = carbohydrates - 30,
                        max = carbohydrates + 30
                    }
                    //fats????
                },
                sections = new RequestSections(request.MealsPerDay > 5 ? 5 : request.MealsPerDay) { }
            }
        };

        var mealPlanResponse = await _edamamClient.GenerateMealPlan(mealplanRequest);

        var mealIdsPerDay = GetTrimmedIdsPerDay(mealPlanResponse);

        //var mealId = "";
        // await _edamamClient.GetRecipeInfo(mealId);

        var mealIds = mealIdsPerDay.Values.SelectMany(x => x).ToList();

        List<Recipe> recipesInDBb = _dbContext.Recipes
                .Where(row => !string.IsNullOrEmpty(row.EdamamId) && mealIds.Contains(row.EdamamId))
                .ToList();

        mealIds.RemoveAll(id => recipesInDBb.Any(row => row.EdamamId == id));

        var recipeSearch = await ExecuteAsyncRecipeSearch(mealIds);

        var recipesToSave = new List<Recipe>();

        foreach (var recipe in recipeSearch)
        {
            var edamamId = GetTrimmedIdFromUrl(recipe.recipe.uri);

            recipesToSave.Add(new Recipe
            {
                Name = recipe.recipe.label,
                EnergyKcal = recipe.recipe.totalNutrients.ENERC_KCAL.quantity,
                Carbohydrates = recipe.recipe.totalNutrients.CHOCDF.quantity,
                Fats = recipe.recipe.totalNutrients.FAT.quantity,
                Protein = recipe.recipe.totalNutrients.PROCNT.quantity,
                EdamamId = edamamId,
                ImageURL = await AddImageToBlobStorage(edamamId, recipe.recipe.image)
            });
        }

        _dbContext.AddRange(recipesToSave);
        _dbContext.SaveChanges();

        _ = recipesInDBb.Concat(recipesToSave);

        recipesInDBb = OrderMealsByDay(recipesInDBb, mealIdsPerDay);

        return new GenerateMealPlanResponse
        {
            Recipes = recipesInDBb
        };
    }

    static Dictionary<int, List<string>> GetTrimmedIdsPerDay(EdamamMealPlanResponse? mealPlanResponse)
    {
        var trimmedIdsPerDay = new Dictionary<int, List<string>>();

        if (mealPlanResponse?.selection is not null)
        {
            var day = 1;
            foreach (var dailyMeals in mealPlanResponse.selection)
            {
                if (dailyMeals.sections is not null)
                {
                    trimmedIdsPerDay.Add(day, new List<string>(GetTrimmedIdsFromSections(dailyMeals.sections)));
                }
                day++;
            }
        }

        return trimmedIdsPerDay;
    }

    static List<string> GetTrimmedIdsFromSections(ResponseSections sections)
    {
        var trimmedIds = new List<string>();

        foreach (var mealProperty in sections.GetType().GetProperties())
        {
            if (mealProperty.PropertyType == typeof(ResponseMeal))
            {
                var meal = mealProperty.GetValue(sections) as ResponseMeal;
                if (!string.IsNullOrEmpty(meal?.assigned))
                {
                    trimmedIds.Add(GetTrimmedIdFromUrl(meal.assigned));
                }
            }
        }

        return trimmedIds;
    }

    static string GetTrimmedIdFromUrl(string url)
    {
        int index = url.IndexOf("#recipe_", StringComparison.OrdinalIgnoreCase);

        if (index is not -1)
        {
            return url[(index + "#recipe_".Length)..];
        }

        return url;
    }

    static List<Recipe> OrderMealsByDay(List<Recipe> allMeals, Dictionary<int, List<string>> mealsByDay)
    {
        var orderedMeals = new List<Recipe>();

        foreach (var day in mealsByDay.Keys.OrderBy(day => day))
        {
            if (mealsByDay.TryGetValue(day, out List<string> mealIdsForDay))
            {
                orderedMeals.AddRange(allMeals.Where(meal => mealIdsForDay.Contains(meal.EdamamId)));
            }
        }

        return orderedMeals;
    }

    async Task<List<RecipeSearchResponse>> ExecuteAsyncRecipeSearch(List<string> mealIdsPerDay)
    {
        var tasks = mealIdsPerDay.Select(id => _edamamClient.GetRecipeInfo(id)).ToList();

        var completedTasks = await Task.WhenAll(tasks);

        return completedTasks.ToList();
    }

    async Task<string> AddImageToBlobStorage(string mealId, string url)
    {
        using HttpClient httpClient = new HttpClient();
        using Stream stream = await httpClient.GetStreamAsync(url);

        return await _blobStorageService.UploadAsync(mealId, stream);
    }
}