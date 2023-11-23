using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Clients
{
    public interface IEdamamClient
    {
        Task<MealPlanResponse?> GenerateMealPlan(MealPlanRequest request);

        Task<RecipeResponse?> GetRecipeInfoByURI(string request);

        Task<RecipeResponse?> GetRecipeInfo(string request);

        Task<string?> GetNutritionData(string request);
    }
}
