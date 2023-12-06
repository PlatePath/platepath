using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Data.Models.MealPlans
{
    public record GenerateMealPlanRequest
    {
        public int Days { get; set; }
        public int MealsPerDay { get; set; }
        public double? MinCalories { get; set; }
        public double? MaxCalories { get; set; }
        public double? Proteins { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Fats { get; set; }
        public string DietType { get; set; }
    }

    public record GenerateMealPlanResponse
    {
        public List<RecipeSearchResponse> Recipes { get; set; }
    }
}