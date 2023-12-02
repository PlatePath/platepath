using Microsoft.AspNetCore.Mvc;
using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;
using PlatePath.API.Services;

namespace PlatePath.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }



    }
}