namespace PlatePath.API.Singleton
{
    public class Configuration
    {
        public string EdamamAppID { get; set; }
        public string EdamamAppKey { get; set; }
        public string EdamamMealPlanner { get; set; }
        public string EdamamRecipeSearch { get; set; }
        public string EdamamRecipeSearchURI { get; set; }
        public string EdamamNutrition { get; set; }
        public JWTConfig JWTConfig { get; set; }
    }

    public class JWTConfig
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}