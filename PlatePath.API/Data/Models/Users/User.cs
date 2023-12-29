using Microsoft.AspNetCore.Identity;
using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Data.Models.Forum;
using PlatePath.API.Data.Models.Genders;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Data.Models.WeightGoals;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Users
{
    public class User : IdentityUser
    {
        public int? GenderId { get; set; }

        [ForeignKey("GenderId")]
        public Gender? Gender { get; set; }

        public int? ActivityLevelId { get; set; }

        [ForeignKey("ActivityLevelId")]
        public ActivityLevel? ActivityLevel { get; set; }

        public int? WeightGoalId { get; set; }

        [ForeignKey("WeightGoalId")]
        public WeightGoal? WeightGoal { get; set; }

        public ICollection<MealPlan> MealPlans { get; init; } = new List<MealPlan>();

        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        
        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public int? Age { get; set; }

        public double? HeightCm { get; set; }

        public double? WeightKg { get; set; }

        public int? NeededCalories { get; set; }

        public int? NeededFats { get; set; }

        public int? NeededCarbs { get; set; }

        public int? NeededProtein { get; set; }
        
        public bool IsBanned { get; set; }
    }
}
