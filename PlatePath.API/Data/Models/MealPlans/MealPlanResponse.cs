namespace PlatePath.API.Data.Models.MealPlans;

/// <summary>
/// Maps a response from Edamam's meal plan generation API (/api/meal-planner/v1/{app_id}/select).
/// Documentation: https://developer.edamam.com/edamam-docs-meal-planner-api
/// The response is essentially a tree of sections with an optional assigned url to them.
/// </summary>
public class MealPlanResponse
{
    public string? status { get; set; }
    public List<Selection> selections { get; set; }
}

public class Selection
{
    public ResponseSections sections { get; set; }
}

public class ResponseSections
{
    public ResponseMeal Meal1 { get; set; }
    public ResponseMeal Meal2 { get; set; }
    public ResponseMeal Meal3 { get; set; }
    public ResponseMeal Meal4 { get; set; }
    public ResponseMeal Meal5 { get; set; }
}

public class ResponseMeal
{
}