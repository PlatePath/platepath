namespace PlatePath.API.Data.Models.MealPlans
{
    public record GenerateMealPlanRequest
    {
        public int Days { get; set; }
        public int MealsPerDay { get; set; }
        public double? MinCalories { get; set; }
        public double? MaxCalories { get; set; }
        public string DietType { get; set; }
    }

    public record GenerateMealPlanResponse
    {

    }
}