namespace PlatePath.API.Data.Models;

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
    public Recipe? Recipe { get; set; }
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

public class Recipe
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

public class Ingredient
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

public class Nutrient
{
    public string? Name { get; set; } // not exactly like in the response, has to be decided how to store this
    public string? Label { get; set; }
    public double Quantity { get; set; }
    public string? Unit { get; set; }
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