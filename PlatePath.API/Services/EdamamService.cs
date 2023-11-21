using Microsoft.Extensions.Options;
using PlatePath.API.Clients;
using PlatePath.API.Singleton;

namespace PlatePath.API.Services
{
    public class EdamamService : IEdamamService
    {
        readonly IEdamamClient _edamamClient;
        readonly ILogger<EdamamService> _logger;
        readonly Configuration _cfg;
        public EdamamService(IEdamamClient edamamClient, ILogger<EdamamService> logger, IOptions<Configuration> cfg)
        {
            _edamamClient = edamamClient;
            _logger = logger;
            _cfg = cfg.Value;
        }

        public async Task<string> GenerateMealPlan() //TODO add params
        {
            //TODO struct edamam request for meal plan from params
            var edamamRequest = "";

            var edamamResponse = await _edamamClient.GenerateMealPlan(edamamRequest);

            //TODO get the mealURIs from the response and call the next edamam api

            return edamamRequest;
        }
    }
}
