using Microsoft.Extensions.Options;
using PlatePath.API.Data.Models;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Services;
using PlatePath.API.Singleton;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace PlatePath.API.Clients
{
    public class EdamamClient : IEdamamClient
    {
        readonly ILogger<EdamamService> _logger;
        readonly Configuration _cfg;

        public EdamamClient(ILogger<EdamamService> logger, IOptions<Configuration> cfg)
        {
            _logger = logger;
            _cfg = cfg.Value;
        }

        public async Task<MealPlanResponse?> GenerateMealPlan(MealPlanRequest request)  // todo add request body
        {
            //var retryPolicy = Policy
            //    .Handle<HttpRequestException>()
            //    .Retry(2, (exception, retryCount) =>
            //    {
            //        _logger.LogError($"GenerateMealPlan: Retry {retryCount} due to {exception.Message} \r\n Request: {request}");
            //    });

            //var client = new HttpClient();

            //var result = await retryPolicy.ExecuteAsync(async () =>
            //{
            //    var response = await client.PostAsync(url, requestMessage);

            //    response.EnsureSuccessStatusCode();

            //    return await response.Content.ReadAsStringAsync();
            //});

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                //.Retry(response  => _logger.LogError($"GenerateMealPlan: Retry {retryCount} due to {exception.Message} \r\n Request: {request}"))
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            var httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await policyWrap.ExecuteAsync(async () =>
                 await httpClient.PostAsJsonAsync(StringifyURL(), request));

            MealPlanResponse? mealPlanResponse = null;

            if (httpResponse.IsSuccessStatusCode)
            {
                mealPlanResponse = await httpResponse.Content.ReadFromJsonAsync<MealPlanResponse>();
            }

            return mealPlanResponse;

            string StringifyURL() => $"{_cfg.EdamamMealPlanner}/{_cfg.EdamamAppID}/select";
        }

        public async Task<RecipeResponse?> GetRecipeInfoByURI(string request)  // todo add request body
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            var httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await policyWrap.ExecuteAsync(async () =>
                 await httpClient.PostAsJsonAsync(StringifyURL(), new RecipeResponse()));  // todo add request body

            RecipeResponse? recipeResponse = null;

            if (httpResponse.IsSuccessStatusCode)
            {
                recipeResponse = await httpResponse.Content.ReadFromJsonAsync<RecipeResponse>();
            }

            return recipeResponse;

            string StringifyURL() => $"{_cfg.EdamamRecipeSearchURI}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        public async Task<RecipeResponse?> GetRecipeInfo(string request)  // todo add request body
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            var httpClient = new HttpClient();

            HttpResponseMessage httpResponse = await policyWrap.ExecuteAsync(async () =>
                 await httpClient.PostAsJsonAsync(StringifyURL(), new RecipeResponse()));  // todo add request body

            RecipeResponse? recipeResponse = null;

            if (httpResponse.IsSuccessStatusCode)
            {
                recipeResponse = await httpResponse.Content.ReadFromJsonAsync<RecipeResponse>();
            }

            return recipeResponse;

            string StringifyURL() => $"{_cfg.EdamamRecipeSearch}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        //TODO
        public async Task<string?> GetNutritionData(string request)
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);

            var httpClient = new HttpClient();

            //HttpResponseMessage httpResponse = await policyWrap.ExecuteAsync(async () =>
            //     await httpClient.PostAsJsonAsync(StringifyURL(), new RecipeResponse()));  // todo add request body

            //RecipeResponse? recipeResponse = null;

            //if (httpResponse.IsSuccessStatusCode)
            //{
            //    recipeResponse = await httpResponse.Content.ReadFromJsonAsync<RecipeResponse>();
            //}

            return "";

            //string Stringify() => $"{_cfg.EdamamNutrition}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }
    }
}
