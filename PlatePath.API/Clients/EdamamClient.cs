using Microsoft.Extensions.Options;
using PlatePath.API.Data.Models;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Services;
using PlatePath.API.Singleton;
using Polly;
using Polly.Extensions.Http;
using Polly.Wrap;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<EdamamMealPlanResponse?> GenerateMealPlan(EdamamMealPlanRequest request)  // todo add request body
        {
            AsyncPolicyWrap<HttpResponseMessage> policyWrap = GetPollyWrap();

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var reqSerialized = JsonSerializer.Serialize(request, options);

            var httpClient = new HttpClient();

            AuthenticateEdamamBasicAuth(httpClient);

            var content = new StringContent(reqSerialized, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponsePolly = await policyWrap.ExecuteAsync(async () =>
                 await httpClient.PostAsync(StringifyURL(), content));

            EdamamMealPlanResponse? mealPlanResponse = null;

            if (httpResponsePolly.IsSuccessStatusCode)
            {
                mealPlanResponse = await httpResponsePolly.Content.ReadFromJsonAsync<EdamamMealPlanResponse>();
            }

            return mealPlanResponse;

            string StringifyURL() => $"{_cfg.EdamamMealPlanner}/{_cfg.EdamamAppID}/select";
        }

        public async Task<RecipeSearchResponse?> GetRecipeInfo(string request)
        {
            try
            {
                AsyncPolicyWrap<HttpResponseMessage> policyWrap = GetPollyWrap();

                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var httpClient = new HttpClient();

                AuthenticateEdamamRecipeAuth(httpClient);

                HttpResponseMessage httpResponse = await policyWrap.ExecuteAsync(async () =>
                     await httpClient.GetAsync(StringifyURL()));

                RecipeSearchResponse? recipeResponse = null;

                if (httpResponse.IsSuccessStatusCode)
                {
                    recipeResponse = await httpResponse.Content.ReadFromJsonAsync<RecipeSearchResponse>();
                    recipeResponse.recipe.CaloriesPerServing = recipeResponse.recipe.calories / recipeResponse.recipe.yield;
                    recipeResponse.recipe.IngredientsString = string.Join("&&", recipeResponse.recipe.ingredientLines);
                }

                return recipeResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed GetRecipeInfo mealIdRequest: {request} \r\n {ex.Message}");
            }

            return null;

            string StringifyURL() => $"{_cfg.EdamamRecipeSearch}/{request}?type=public&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        //TODO
        public async Task<string?> GetNutritionData(string request)
        {
            AsyncPolicyWrap<HttpResponseMessage> policyWrap = GetPollyWrap();

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

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

        void AuthenticateEdamamBasicAuth(HttpClient httpClient)
        {
            var authenticationString = $"{_cfg.EdamamAppID}:{_cfg.EdamamAppKey}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            httpClient.DefaultRequestHeaders.Add("Edamam-Account-User", _cfg.EdamamAccountUser);
        }

        void AuthenticateEdamamRecipeAuth(HttpClient httpClient)
        {
            var authenticationString = $"{_cfg.EdamamAppID}:{_cfg.EdamamAppKey}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Edamam-Account-User", _cfg.EdamamAccountUser);
            httpClient.DefaultRequestHeaders.Add("Accept-Language","en");
        }

        static AsyncPolicyWrap<HttpResponseMessage> GetPollyWrap()
        {
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
                //.Retry(response  => _logger.LogError($"GenerateMealPlan: Retry {retryCount} due to {exception.Message} \r\n Request: {request}"))
                .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            var circuitBreakerPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

            var policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
            return policyWrap;
        }
    }
}
