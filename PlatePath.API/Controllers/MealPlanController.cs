using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlatePath.API.Data.Models;
using PlatePath.API.Data.Models.Authentication;
using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;
using PlatePath.API.Services;
using PlatePath.API.Singleton;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlatePath.API.Controllers
{

    [ApiController]
    [Route("api/mealplan")]
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
        public async Task<IActionResult> GenerateMealPlan([FromBody] LoginUser loginUser) //TODO add request params
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _edamamService.GenerateMealPlan()); //TODO add params
        }
    }
}