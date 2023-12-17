using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Data.Models.MealPlans
{
    public class MealPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ICollection<Recipe> Meals { get; set; } = new List<Recipe>();
    }    
}