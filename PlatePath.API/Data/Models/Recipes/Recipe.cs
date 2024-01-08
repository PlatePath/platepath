using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PlatePath.API.Data.Models.Forum;
using PlatePath.API.Data.Models.MealPlans;


namespace PlatePath.API.Data.Models.Recipes
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public Post? Post { get; set; }

        [JsonIgnore]
        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        
        public string? EdamamId { get; set; }
        
        public string Name { get; set; }
        
        public int Kcal { get; set; }

        public int Servings { get; set; }

        public int Carbohydrates { get; set; }
        
        public int Fats { get; set; }
        
        public int Protein { get; set; }

        public string IngredientLines { get; set; }

        public string? ImageURL { get; set; }
    }
}