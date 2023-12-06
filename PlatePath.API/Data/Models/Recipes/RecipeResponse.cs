using PlatePath.API.Data.Models.Recipes;

namespace PlatePath.API.Data.Models.Recipes;

/// <summary>
/// Maps a response from Edamam's recipe search API (/api/recipes/v2).
/// Documentation: https://developer.edamam.com/edamam-docs-recipe-api#/
/// </summary>
public class RecipeResponse
{
    public int From { get; set; }
    public int To { get; set; }
    public int Count { get; set; }
    public Links? Links { get; set; }
    public Hit[]? Hits { get; set; }
}

public class Hit
{
    public RecipeDescription? Recipe { get; set; }
    public Links? Links { get; set; }
}

public class Links
{
    public Link? Self { get; set; }
    public Link? Next { get; set; }
}

public class Link
{
    public string? Href { get; set; }
    public string? Title { get; set; }
}

public class RecipeDescription
{
    public string? Uri { get; set; }
    public string? Label { get; set; }
    public string? Image { get; set; }
    public InlineModel? Images { get; set; }
    public string? Source { get; set; }
    public string? Url { get; set; }
    public string? ShareAs { get; set; }
    public double Yield { get; set; }
    public List<string>? DietLabels { get; set; }
    public List<string>? HealthLabels { get; set; }
    public List<string>? Cautions { get; set; }
    public List<string>? IngredientLines { get; set; }
    public List<Ingredient>? Ingredients { get; set; }
    public double Calories { get; set; }
    public double GlycemicIndex { get; set; }
    public double TotalCo2Emissions { get; set; }
    public string? Co2EmissionsClass { get; set; }
    public double TotalWeight { get; set; }
    public List<string>? CuisineType { get; set; }
    public List<string>? MealType { get; set; }
    public List<string>? DishType { get; set; }
    public List<string>? Instructions { get; set; }
    public List<string>? Tags { get; set; }
    public string? ExternalId { get; set; }
    public NutrientsInfo? TotalNutrients { get; set; }
    public NutrientsInfo? TotalDaily { get; set; }
    public List<Digest>? Digest { get; set; }
}

public class InlineModel
{
    public ImageInfo? Thumbnail { get; set; }
    public ImageInfo? Small { get; set; }
    public ImageInfo? Regular { get; set; }
    public ImageInfo? Large { get; set; }
}

public class ImageInfo
{
    public string? Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class RecipeIngredient
{
    public string? Text { get; set; }
    public double Quantity { get; set; }
    public string? Measure { get; set; }
    public string? Food { get; set; }
    public double Weight { get; set; }
    public string? FoodId { get; set; }
}

public class NutrientsInfo
{
    public Nutrient[]? Nutrients { get; set; }
}

public class Digest
{
    public DigestEntry? DigestEntry { get; set; }
}

public class DigestEntry
{
    public string? Label { get; set; }
    public string? Tag { get; set; }
    public string? SchemaOrgTag { get; set; }
    public double Total { get; set; }
    public bool HasRdi { get; set; }
    public double Daily { get; set; }
    public string? Unit { get; set; }
    public Digest? Sub { get; set; }
}

public class RecipeSearchResponse
{
    public RecipeBody recipe { get; set; }
}

public class RecipeBody
{
    public string label { get; set; }
    public string image { get; set; }
    public List<string> ingredientLines { get; set; }
    public double calories { get; set; }
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
    public string? label { get; set; }
    public double quantity { get; set; }
    public string? unit { get; set; }
}