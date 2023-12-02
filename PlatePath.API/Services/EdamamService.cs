using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Singleton;

namespace PlatePath.API.Services
{
    public class EdamamService : IEdamamService
    {
        readonly IEdamamClient _edamamClient;
        readonly ILogger<EdamamService> _logger;
        readonly Configuration _cfg;
        public EdamamService(IEdamamClient edamamClient, ILogger<EdamamService> logger, IOptions<Configuration> cfg)
        {
            _edamamClient = edamamClient;
            _logger = logger;
            _cfg = cfg.Value;
        }

        public async Task<GenerateMealPlanResponse> GenerateMealPlan(GenerateMealPlanRequest request) //TODO add params
        {
            Dictionary<string, RequestMeal> Meals = new Dictionary<string, RequestMeal>();
            for (int i = 1; i <= request.MealsPerDay; i++)
            {
                Meals[$"Meal{i}"] = new RequestMeal { };
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
                            min = request.MinCalories,
                            max = request.MaxCalories
                        }
                    },
                    sections = new RequestSections
                    {
                        Meal1
                    }

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
}
