using Microsoft.EntityFrameworkCore;
using PlatePath.API.Data;
using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Data.Models.Profile;
using PlatePath.API.Enums;
using PlatePath.API.Models.InputModels.User;

namespace PlatePath.API.Services;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _context;

    public ProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserPersonalDataModel GetUserPersonalData(string userId)
    {
        var user = _context.Users.Include(x => x.Gender)
                                 .Include(x => x.ActivityLevel)
                                 .Include(x => x.WeightGoal)
                                 .FirstOrDefault(x => x.Id == userId);

        if (user == null)
        {
            return null;
        }

        return new UserPersonalDataModel
        {
            ActivityLevel = user.ActivityLevel.Name,
            Age = user.Age ?? 0,
            Gender = user.Gender.Name,
            HeightCm = user.HeightCm ?? 0,
            WeightGoal = user.WeightGoal.Name,
            WeightKg = user.WeightKg ?? 0,
        };
    }

    public bool SetUserPersonalData(UserPersonalDataModel request, string userId)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            return false;
        }

        user.Age = request.Age;
        user.WeightKg = request.WeightKg;
        user.HeightCm = request.HeightCm;
        user.Gender = _context.Genders.Where(x => x.Name == request.Gender).First();
        user.ActivityLevel = _context.ActivityLevel.Where(x => x.Name == request.ActivityLevel).First();
        user.WeightGoal = _context.WeightGoal.Where(x => x.Name == request.WeightGoal).First();

        return _context.SaveChanges() > 0;
    }

    public NutritionCalculationResult CalculateNutrition(string userId)
    {
        var user = _context.Users.Where(u => u.Id == userId).Include(x => x.Gender).Include(x => x.ActivityLevel).Include(x => x.WeightGoal).FirstOrDefault();
        if (user == null)
            throw new ArgumentException("User not found.", nameof(userId));

        if (!user.WeightKg.HasValue || !user.HeightCm.HasValue || !user.Age.HasValue ||
            user.Gender == null || user.ActivityLevel == null || user.WeightGoal == null)
            throw new ArgumentException("Missing user data.");

        double bmr = CalculateBMR(user.WeightKg.Value, user.HeightCm.Value, user.Age.Value, user.Gender.Name);
        double tdee = CalculateTDEE(bmr, ParseActivityLevel(user.ActivityLevel.Name));
        WeightGoalEnum weightGoal = ParseWeightGoal(user.WeightGoal.Name);

        switch (weightGoal)
        {
            case WeightGoalEnum.LooseWeight:
                tdee *= 0.90;
                break;
            case WeightGoalEnum.GainWeight:
                tdee *= 1.10;
                break;
        }

        var (proteinGrams, fatGrams, carbGrams) = CalculateMacros(tdee);

        return new NutritionCalculationResult
        {
            Calories = Math.Round(tdee),
            ProteinGrams = Math.Round(proteinGrams),
            FatGrams = Math.Round(fatGrams),
            CarbGrams = Math.Round(carbGrams)
        };
    }

    private double CalculateBMR(double weight, double height, int age, string gender)
    {
        return gender.ToLower() == "male" ?
            10 * weight + 6.25 * height - 5 * age + 5 :
            10 * weight + 6.25 * height - 5 * age - 161;
    }

    private double CalculateTDEE(double bmr, ActivityLevelEnum activityLevel)
    {
        return activityLevel switch
        {
            ActivityLevelEnum.Sedentary => bmr * 1.2,
            ActivityLevelEnum.LightlyActive => bmr * 1.375,
            ActivityLevelEnum.ModeratelyActive => bmr * 1.55,
            ActivityLevelEnum.VeryActive => bmr * 1.725,
            ActivityLevelEnum.ExtraActive => bmr * 1.9,
            _ => bmr
        };
    }

    private (double ProteinGrams, double FatGrams, double CarbGrams) CalculateMacros(double tdee)
    {
        double proteinCalories = tdee * 0.30;
        double fatCalories = tdee * 0.25;
        double carbCalories = tdee - proteinCalories - fatCalories;
        return (proteinCalories / 4, fatCalories / 9, carbCalories / 4);
    }

    private ActivityLevelEnum ParseActivityLevel(string activityLevelName)
    {
        return Enum.TryParse(activityLevelName, out ActivityLevelEnum activityLevel) ? activityLevel : ActivityLevelEnum.Sedentary;
    }

    private WeightGoalEnum ParseWeightGoal(string weightGoalName)
    {
        return Enum.TryParse(weightGoalName, out WeightGoalEnum weightGoal) ? weightGoal : WeightGoalEnum.MaintainWeight;
    }


}