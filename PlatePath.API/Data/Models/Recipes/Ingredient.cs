using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Recipes
{
    public class Ingredient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        
        public string Name { get; set; }
        
        public double Quantity { get; set; }
        
        public string Measure { get; set; }
    }
}