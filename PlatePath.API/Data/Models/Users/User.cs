using Microsoft.AspNetCore.Identity;
using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Data.Models.Genders;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Data.Models.WeightGoals;

namespace PlatePath.API.Data.Models.Users
{
    public class User : IdentityUser
    {
        public ICollection<MealPlan> MealPlans { get; init; } = new List<MealPlan>();

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        
        public Gender Gender { get; set; }

        public ActivityLevel ActivityLevel { get; set; }

        public WeightGoal WeightGoal { get; set; }
        
        public ICollection<MealPlan> MealPlans { get; init; } = new List<MealPlan>();

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public int? Age { get; set; }

        public double? HeightCm { get; set; }

        public double? WeightKg { get; set; }

        public double? NeededCalories { get; set; }

        public double? NeededFats { get; set; }

        public double? NeededCarbs { get; set; }

        public double? NeededProtein { get; set; }
    }
}
