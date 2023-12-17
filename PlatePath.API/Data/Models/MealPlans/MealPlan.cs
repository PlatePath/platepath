using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Singleton;

namespace PlatePath.API.Data.Models.MealPlans;

public class MealPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public ICollection<Recipe> Meals { get; set; } = new List<Recipe>();
}

public record MealPlanResponse : BaseResponse
{
    public MealPlanResponse() { }
    public MealPlanResponse(ErrorCode error) : base(error) { }
    public MealPlan MealPlan { get; set; }
}