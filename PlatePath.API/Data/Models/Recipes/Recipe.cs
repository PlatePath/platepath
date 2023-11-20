using System.ComponentModel.DataAnnotations.Schema;

namespace PlatePath.API.Data.Models.Recipes
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> DietLabels { get; set; }
        public double EnergyKcal { get; set; }
        public double Carbohydrates { get; set; }
        public double Fats { get; set; }
        public double Protein { get; set; }
    }
}