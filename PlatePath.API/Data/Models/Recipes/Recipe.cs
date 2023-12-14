using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public ICollection<DietLabel> DietLabels { get; set; } = new List<DietLabel>();
        
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        
        public string? EdamamId { get; set; }
        
        public string Name { get; set; }
        
        public double EnergyKcal { get; set; }
        
        public double Carbohydrates { get; set; }
        
        public double Fats { get; set; }
        
        public double Protein { get; set; }

        public string? ImageURL { get; set; }
    }
}