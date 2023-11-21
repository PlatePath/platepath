namespace PlatePath.API.Data.Models.MealPlans;

public class MealPlanRequest
{
    public int size { get; set; }
    public Plan plan { get; set; }
}

public class Plan
{
    public Accept accept { get; set; }
    public Fit fit { get; set; }
    public ResponseSections sections { get; set; }
}

public class Accept
{
    public List<All> all { get; set; }
}

public class All
{
    public List<string> health { get; set; }
}

public class Fit
{
    public ENERCKCAL ENERC_KCAL { get; set; }
}

public class ENERCKCAL
{
    public int min { get; set; }
    public int max { get; set; }
}

public class RequestSections
{
    public RequestMeal Meal1 { get; set; }
    public RequestMeal Meal2 { get; set; }
    public RequestMeal Meal3 { get; set; }
    public RequestMeal Meal4 { get; set; }
    public RequestMeal Meal5 { get; set; }
}

public class RequestMeal
{
}