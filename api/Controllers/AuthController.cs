using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;
using PlatePath.API.Services;

namespace PlatePath.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginUser)
        {
            var loginResponse = await _authService.Login(loginUser);
            if (!loginResponse.Success)
                return Unauthorized();

            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var registerResponse = await _authService.Register(request);
            if (!registerResponse.Success)
                return StatusCode(StatusCodes.Status409Conflict, registerResponse);

            return Ok(registerResponse);
        }

    }
}