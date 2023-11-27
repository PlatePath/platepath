using PlatePath.API.Data.Models.Profile;

namespace PlatePath.API.Services
{
    public interface IProfileService
    {
        NutritionCalculationResult CalculateNutrition(string userId);
    }
}
