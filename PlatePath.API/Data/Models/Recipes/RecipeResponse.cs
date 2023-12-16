namespace PlatePath.API.Data.Models.Recipes;

public class RecipeSearchResponse
{
    public RecipeBody recipe { get; set; }
}

public class RecipeBody
{
    public string uri { get; set; }
    public string label { get; set; }
    public string image { get; set; }
    public List<string> ingredientLines { get; set; }
    public string IngredientsString { get; set; }
    public double yield { get; set; }
    public double calories { get; set; }
    public double CaloriesPerServing { get; set; }
    public TotalNutrients totalNutrients { get; set; }
}

public class TotalNutrients
{
    public Nutrient ENERC_KCAL { get; set; }
    public Nutrient FAT { get; set; }
    public Nutrient CHOCDF { get; set; }
    public Nutrient PROCNT { get; set; }
}

public class Nutrient
{
    public double quantity { get; set; }
}