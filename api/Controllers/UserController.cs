using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Models.InputModels.User;
using PlatePath.API.Services;
using System.Security.Claims;

namespace PlatePath.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        readonly IProfileService _profileService;

        public UserController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("setPersonalData")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SetPersonalData([FromBody] UserPersonalDataInputModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return BadRequest();

            var isSaved = _profileService.SetUserPersonalData(request, userId);

            if (!isSaved)
            {
                return StatusCode(500);
            }

            return Ok(isSaved);
        }

        [HttpGet("getNeededNutrition")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetNeededNutrition()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return BadRequest();

            var nutritionData = _profileService.CalculateNutrition(userId);

            return Ok(nutritionData);
        }
    }
}