using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Singleton;
using System.Dynamic;

namespace PlatePath.API.Services;

public class EdamamService : IEdamamService
{
    readonly IEdamamClient _edamamClient;
    readonly ILogger<EdamamService> _logger;
    readonly Configuration _cfg;
    readonly IProfileService _profileService;
    public EdamamService(IEdamamClient edamamClient, IProfileService profileService, ILogger<EdamamService> logger, IOptions<Configuration> cfg)
    {
        _edamamClient = edamamClient;
        _profileService = profileService;
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

        if (request.MealsPerDay > 5)
            request.MealsPerDay = 5;

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
                        min = request.MinCalories ?? 1000,
                        max = request.MaxCalories ?? 5000
                    }
                    ,
                    PROCNT = new MinMaxQuantity
                    {
                        min = request.Proteins ?? 70,
                        max = request.Proteins ?? 200
                    },
                    CHOCDF = new MinMaxQuantity
                    {
                        min = request.Carbohydrates ?? 70,
                        max = request.Carbohydrates ?? 200
                    }
                    //fats????
                },
                sections = new RequestSections(request.MealsPerDay > 5 ? 5 : request.MealsPerDay) { }
            }
        };

        var mealPlanResponse = await _edamamClient.GenerateMealPlan(mealplanRequest);

        var mealIdsPerDay = GetTrimmedIdsPerDay(mealPlanResponse);

        var mealId = "";
        await _edamamClient.GetRecipeInfo(mealId);

        var recipeSearch = ExecuteAsyncRecipeSearch(mealIdsPerDay);

        return new GenerateMealPlanResponse
        {

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

    async Task<List<RecipeSearchResponse>> ExecuteAsyncRecipeSearch(Dictionary<int, List<string>> mealIdsPerDay)
    {
        var tasks = new List<Task<RecipeSearchResponse>>();

        foreach (var idsForDay in mealIdsPerDay.Values)
        {
            tasks.AddRange(idsForDay.Select(id => _edamamClient.GetRecipeInfo(id)));
        }

        await Task.WhenAll(tasks);

        return new List<RecipeSearchResponse>(tasks.Select(x => x.Result)).ToList();
    }
}