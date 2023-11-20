using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Recipe
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public double EnergyKcal { get; set; }
        
        public double Carbohydrates { get; set; }
        
        public double Fats { get; set; }
        
        public double Protein { get; set; }
        
        public List<DietLabel> DietLabels { get; set; } = new();
        
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}