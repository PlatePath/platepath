using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.Profile;
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
        public async Task<IActionResult> SetPersonalData([FromBody] UserPersonalDataModel request)
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

        [HttpPost("getPersonalData")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPersonalData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return BadRequest();

            var personalData = _profileService.GetUserPersonalData(userId);

            if (personalData == null)
            {
                return StatusCode(500);
            }

            return Ok(personalData);
        }

        [HttpGet("getNeededNutrition")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetNeededNutrition()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return BadRequest();

            NutritionCalculationResult nutritionData = null;

            try
            {
                nutritionData = _profileService.CalculateNutrition(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(nutritionData);
        }
    }
}