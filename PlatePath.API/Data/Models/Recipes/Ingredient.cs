using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Recipes
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public double Quantity { get; set; }
        
        public string Measure { get; set; }
    }
}