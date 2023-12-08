using PlatePath.API.Data.Models.Profile;
using PlatePath.API.Models.InputModels.User;

namespace PlatePath.API.Services;

public interface IProfileService
{
    NutritionCalculationResult CalculateNutrition(string userId);

    bool SetUserPersonalData(UserPersonalDataModel request, string userId);

    UserPersonalDataModel GetUserPersonalData(string userId);
}