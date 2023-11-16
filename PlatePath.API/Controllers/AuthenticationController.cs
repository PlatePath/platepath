using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PlatePath.API.Data.Models;
using PlatePath.API.Data.Models.Authentication;
using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlatePath.API.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            var user = await _userManager.FindByNameAsync(loginUser.Username);
            if (user is not null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var userExists = await _userManager.FindByNameAsync(registerUser.Username);
            if (userExists is not null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User already exists!" });

            var user = new IdentityUser
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault()?.Description;
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = $"Creating User failed! Error:{error}" });
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("register-admin")]  
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUser registerAdmin)
        {
            var userExists = await _userManager.FindByNameAsync(registerAdmin.Username);
            if (userExists is not null)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User already exists!" });

            var user = new IdentityUser
            {
                Email = registerAdmin.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerAdmin.Username
            };
            var result = await _userManager.CreateAsync(user, registerAdmin.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}