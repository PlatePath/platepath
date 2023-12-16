using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Data;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Singleton;
using System;
using System.Collections;
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

    public async Task<GenerateMealPlanResponse> GenerateMealPlan(string userId, GenerateMealPlanRequest request)
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
        var fats = request.Fats ??= 130;

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
                    },
                    FASAT = new MinMaxQuantity
                    {
                        min = fats - 30,
                        max = fats + 30
                    }
                },
                sections = new RequestSections(request.MealsPerDay > 5 ? 5 : request.MealsPerDay) { }
            }
        };

        var mealPlanResponse = await _edamamClient.GenerateMealPlan(mealplanRequest);

        var mealIdsPerDay = GetTrimmedIdsPerDay(mealPlanResponse);

        var mealIds = mealIdsPerDay.Values.SelectMany(x => x).ToList();

        List<Recipe> recipesInDBb = _dbContext.Recipes
                .Where(row => !string.IsNullOrEmpty(row.EdamamId) && mealIds.Contains(row.EdamamId))
                .ToList();

        if (recipesInDBb.Count > 0)
            mealIds.RemoveAll(id => recipesInDBb.Any(row => row.EdamamId == id));

        var recipesToSave = new List<Recipe>();

        if (mealIds.Count > 0)
        {
            var recipeSearch = await ExecuteAsyncRecipeSearch(mealIds);
            foreach (var recipe in recipeSearch)
            {
                if (recipe is not null)
                {
                    var edamamId = GetTrimmedIdFromUrl(recipe.recipe.uri);

                    recipesToSave.Add(new Recipe
                    {
                        Name = recipe.recipe.label,
                        Kcal = (int)Math.Ceiling(recipe.recipe.totalNutrients.ENERC_KCAL.quantity),
                        KcalPerServing = (int)Math.Ceiling(recipe.recipe.CaloriesPerServing),
                        Servings = (int)Math.Ceiling(recipe.recipe.yield),
                        IngredientLines = recipe.recipe.IngredientsString,
                        Carbohydrates = (int)Math.Ceiling(recipe.recipe.totalNutrients.CHOCDF.quantity),
                        Fats = (int)Math.Ceiling(recipe.recipe.totalNutrients.FAT.quantity),
                        Protein = (int)Math.Ceiling(recipe.recipe.totalNutrients.PROCNT.quantity),
                        EdamamId = edamamId,
                        ImageURL = await AddImageToBlobStorage(edamamId, recipe.recipe.image)
                    });
                }
            }
        }

        if (recipesToSave.Any())
        {
            _dbContext.AddRange(recipesToSave);
            _dbContext.SaveChanges();
            recipesInDBb.AddRange(recipesToSave);
        }

        recipesInDBb = OrderMealsByDay(recipesInDBb, mealIdsPerDay);

        return new GenerateMealPlanResponse
        {
            Recipes = recipesInDBb
        };
    }

    Dictionary<int, List<string>> GetTrimmedIdsPerDay(EdamamMealPlanResponse? mealPlanResponse)
    {
        var trimmedIdsPerDay = new Dictionary<int, List<string>>();

        if (mealPlanResponse?.selection is not null)
        {
            var day = 1;
            foreach (var dailyMeals in mealPlanResponse.selection)
            {
                if (dailyMeals.sections is not null)
                {
                    var trimmedIds = GetTrimmedIdsFromSections(dailyMeals.sections);
                    trimmedIdsPerDay.Add(day, trimmedIds);
                }
                day++;
            }
        }

        return trimmedIdsPerDay;
    }

    List<string> GetTrimmedIdsFromSections(ResponseSections sections)
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

    string GetTrimmedIdFromUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return string.Empty;
        }

        int index = url.IndexOf("#recipe_", StringComparison.OrdinalIgnoreCase);

        if (index != -1)
        {
            return url[(index + "#recipe_".Length)..];
        }

        return url;
    }

    List<Recipe> OrderMealsByDay(List<Recipe> allMeals, Dictionary<int, List<string>> mealsByDay)
    {
        var orderedMeals = new List<Recipe>();

        foreach (var day in mealsByDay.Keys.OrderBy(day => day))
        {
            if (mealsByDay.TryGetValue(day, out List<string>? mealIdsForDay))
            {
                if (mealIdsForDay is not null)
                    orderedMeals.AddRange(allMeals.Where(meal => mealIdsForDay.Contains(meal.EdamamId)));
            }
        }

        return orderedMeals;
    }

    async Task<List<RecipeSearchResponse?>> ExecuteAsyncRecipeSearch(List<string> mealIdsPerDay)
    {
        try
        {
            var tasks = mealIdsPerDay.Select(id => _edamamClient.GetRecipeInfo(id)).ToList();

            var completedTasks = await Task.WhenAll(tasks);

            return completedTasks?.ToList() ?? new List<RecipeSearchResponse?>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get recipe info edamam \r\n {ex.Message}");
        }

        return new List<RecipeSearchResponse?>();
    }

    async Task<string> AddImageToBlobStorage(string mealId, string mealUrl)
    {
        try
        {
            using HttpClient httpClient = new HttpClient();
            using Stream stream = await httpClient.GetStreamAsync(mealUrl);

            return await _blobStorageService.UploadAsync(mealId, stream);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to upload recipe image mealId: {mealId} mealUrl: {mealUrl} \r\n {ex.Message}");
            // throw new ImageUploadException("Failed to upload image", ex);
            return string.Empty;
        }
    }

}