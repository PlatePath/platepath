using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Services;

namespace PlatePath.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MealPlanController : ControllerBase
    {
        readonly IEdamamService _edamamService;

        public MealPlanController(IEdamamService edamamService)
        {
            _edamamService = edamamService;
        }

        [HttpPost("generate")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GenerateMealPlan([FromBody] GenerateMealPlanRequest request)
        {
            return Ok(await _edamamService.GenerateMealPlan(request));
        }
    }
}