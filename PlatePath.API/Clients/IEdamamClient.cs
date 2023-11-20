namespace PlatePath.API.Clients
{
    public interface IEdamamClient
    {
        Task<string?> GenerateMealPlan(string request);

        Task<string?> GetRecipeInfoByURI(string request);

        Task<string?> GetRecipeInfo(string request);

        Task<string?> GetNutritionData(string request);
    }
}
