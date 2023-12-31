﻿using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Singleton;
using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Data.Models.MealPlans
{
    public record GenerateMealPlanRequest
    {
        [Required]
        public string MealPlanName { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public int MealsPerDay { get; set; }
        public double? MinCalories { get; set; }
        public double? MaxCalories { get; set; }
        [Range(10, 269.9)]
        public double? Proteins { get; set; }
        [Range(10, 269.9)]
        public double? Carbohydrates { get; set; }
        [Range(10, 269.9)]
        public double? Fats { get; set; }
        public string? DietType { get; set; }
    }

    public record GenerateMealPlanResponse : BaseResponse
    {
        public GenerateMealPlanResponse() { }
        public GenerateMealPlanResponse(ErrorCode error) : base(error) { }
        public List<Recipe>? Recipes { get; set; }
    }
}