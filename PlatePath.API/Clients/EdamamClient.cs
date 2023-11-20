using Microsoft.Extensions.Options;
using PlatePath.API.Data.Models;
using PlatePath.API.Singleton;
using Polly;
using System.Net;

namespace PlatePath.API.Clients
{
    public class EdamamClient : IEdamamClient
    {
        readonly Configuration _cfg;

        public EdamamClient(IOptions<Configuration> cfg)
        {
            _cfg = cfg.Value;
        }

        const string PostMethod = "POST";
        const string GetMethod = "GET";

        public async Task<string?> GenerateMealPlan(string request)
        {
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Retry {retryCount} due to {exception.Message}");
                });

            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(PostMethod),
                RequestUri = new Uri(Stringify()),
            };

            var client = new HttpClient();

            var result = await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            });

            return result;

            string Stringify() => $"{_cfg.EdamamMealPlanner}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        public async Task<string?> GetRecipeInfoByURI(string request)
        {
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Retry {retryCount} due to {exception.Message}");
                });

            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(GetMethod),
                RequestUri = new Uri(Stringify()),
            };

            var client = new HttpClient();

            var result = await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            });

            return result;

            string Stringify() => $"{_cfg.EdamamRecipeSearchURI}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        public async Task<string?> GetRecipeInfo(string request)
        {
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Retry {retryCount} due to {exception.Message}");
                });

            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(GetMethod),
                RequestUri = new Uri(Stringify()),
            };

            var client = new HttpClient();

            var result = await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            });

            return result;

            string Stringify() => $"{_cfg.EdamamRecipeSearch}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }

        public async Task<string?> GetNutritionData(string request)
        {
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Retry(3, (exception, retryCount) =>
                {
                    Console.WriteLine($"Retry {retryCount} due to {exception.Message}");
                });

            var requestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod(GetMethod),
                RequestUri = new Uri(Stringify()),
            };

            var client = new HttpClient();

            var result = await retryPolicy.ExecuteAsync(async () =>
            {
                var response = await client.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            });

            return result;

            string Stringify() => $"{_cfg.EdamamNutrition}{request}&app_id={_cfg.EdamamAppID}&app_key={_cfg.EdamamAppKey}";
        }
    }
}
