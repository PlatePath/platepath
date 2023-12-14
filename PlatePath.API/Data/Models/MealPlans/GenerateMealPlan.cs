using PlatePath.API.Data.Models.Recipes;
using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Data.Models.MealPlans
{
    public record GenerateMealPlanRequest
    {
        public int Days { get; set; }
        public int MealsPerDay { get; set; }
        public double? MinCalories { get; set; }
        public double? MaxCalories { get; set; }
        [Range(80.1, 269.9)]
        public double? Proteins { get; set; }
        [Range(80.1, 269.9)]
        public double? Carbohydrates { get; set; }
        [Range(80.1, 269.9)]
        public double? Fats { get; set; }
        public string DietType { get; set; }
    }

    public record GenerateMealPlanResponse
    {
        public List<Recipe> Recipes { get; set; }
    }
}