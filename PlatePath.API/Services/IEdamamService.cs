using PlatePath.API.Data.Models.MealPlans;

namespace PlatePath.API.Services
{
    public interface IEdamamService
    {
        Task<string> GenerateMealPlan(MealPlanRequest request);
    }
}
