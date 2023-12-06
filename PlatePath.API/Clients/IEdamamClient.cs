using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Clients
{
    public interface IEdamamClient
    {
        Task<EdamamMealPlanResponse?> GenerateMealPlan(EdamamMealPlanRequest request);

        Task<RecipeResponse?> GetRecipeInfoByURI(string request);

        Task<RecipeSearchResponse?> GetRecipeInfo(string request);

        Task<string?> GetNutritionData(string request);
    }
}
