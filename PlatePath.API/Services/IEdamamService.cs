using PlatePath.API.Data.Models.MealPlans;

namespace PlatePath.API.Services
{
    public interface IEdamamService
    {
        Task<GenerateMealPlanResponse> GenerateMealPlan(GenerateMealPlanRequest request);
    }
}
