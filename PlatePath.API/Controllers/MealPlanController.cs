using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Services;
using System.Security.Claims;

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return ValidationProblem();

            return Ok(await _edamamService.GenerateMealPlan(userId, request));
        }
    }
}