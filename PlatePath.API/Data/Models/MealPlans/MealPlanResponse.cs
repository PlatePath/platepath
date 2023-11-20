namespace PlatePath.API.Data.Models.MealPlans;

/// <summary>
/// Maps a response from Edamam's meal plan generation API (/api/meal-planner/v1/{app_id}/select).
/// Documentation: https://developer.edamam.com/edamam-docs-meal-planner-api
/// The response is essentially a tree of sections with an optional assigned url to them.
/// </summary>
public class MealPlanResponse
{
    public string? Status { get; set; }
    public Selection[]? Selection { get; set; }
}

public class Selection
{
    public Section? Sections { get; set; }
}

public class Section
{
    public string? AssignedUrl { get; set; }
    public Section? ChildSection { get; set; }
}