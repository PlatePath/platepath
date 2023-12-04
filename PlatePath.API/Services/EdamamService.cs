using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Singleton;
using System.Dynamic;

namespace PlatePath.API.Services;

public class EdamamService : IEdamamService
{
    readonly IEdamamClient _edamamClient;
    readonly ILogger<EdamamService> _logger;
    readonly Configuration _cfg;
    readonly IProfileService _profileService;
    public EdamamService(IEdamamClient edamamClient,IProfileService profileService, ILogger<EdamamService> logger, IOptions<Configuration> cfg)
    {
        _edamamClient = edamamClient;
        _profileService = profileService;
        _logger = logger;
        _cfg = cfg.Value;
    }

    public async Task<GenerateMealPlanResponse> GenerateMealPlan(string userId, GenerateMealPlanRequest request) //TODO add params
    {
        if(request.MaxCalories is null || request.MinCalories is null)
        {
            var nutritionsResponse = _profileService.CalculateNutrition(userId);
            if(nutritionsResponse is not null) 
            {
                request.MinCalories = nutritionsResponse.Calories - 100;
                request.MaxCalories = nutritionsResponse.Calories + 100;
            }
        }

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
                    ENERC_KCAL = new ENERCKCAL
                    {
                        min = request.MinCalories ?? 1000,
                        max = request.MaxCalories ?? 5000
                    }
                },
                sections = new RequestSections(3) { }
            }
        };

        var mealPlanResponse = await _edamamClient.GenerateMealPlan(mealplanRequest);

        //TODO get the mealURIs from the response check if we have it in db or call the recipe edamam api

        // multiple recipes on 1 request? 

        var recipeResponse = await _edamamClient.GetRecipeInfoByURI("");

        return new GenerateMealPlanResponse
        {

        };
    }
}