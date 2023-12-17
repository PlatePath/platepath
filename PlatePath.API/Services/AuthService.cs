using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlatePath.API.Data;
using PlatePath.API.Data.Models.Authentication;
using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;
using PlatePath.API.Data.Models.Users;
using PlatePath.API.Singleton;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using PlatePath.API.Data.Models.WeightGoals;
using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Data.Models.Genders;

namespace PlatePath.API.Services;

public class AuthService : IAuthService
{
    readonly ApplicationDbContext _dbContext;
    readonly UserManager<User> _userManager;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly ILogger<AuthService> _logger;
    readonly Configuration _cfg;

    public AuthService(ApplicationDbContext dbContext,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AuthService> logger,
        IOptions<Configuration> cfg)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _cfg = cfg.Value;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user is not null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return new LoginResponse(ErrorCode.OK)
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
        return new LoginResponse(ErrorCode.AuthenticationError);
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists is not null)
            return new RegisterResponse(ErrorCode.UserAlreadyExists);

        WeightGoal? weightGoal = request.WeightGoal != 0 
            ? _dbContext.WeightGoal.FirstOrDefault(wg => wg.Id == request.WeightGoal) 
            : null;
        ActivityLevel? activityLevel = request.ActivityLevel != 0 
            ? _dbContext.ActivityLevel.FirstOrDefault(wg => wg.Id == request.ActivityLevel) 
            : null;
        Gender? gender = request.Gender != 0
            ? _dbContext.Genders.FirstOrDefault(wg => wg.Id == request.Gender) 
            : null;

        var user = new User
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Age = request.Age,
            HeightCm = request.HeightCm,
            WeightKg = request.WeightKg,
            WeightGoal = weightGoal,
            ActivityLevel = activityLevel,
            Gender = gender,
            IsBanned = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return new RegisterResponse(ErrorCode.CreateUserFailed);

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        if (await _roleManager.RoleExistsAsync(UserRoles.User))
            await _userManager.AddToRoleAsync(user, UserRoles.User);

        return new RegisterResponse(ErrorCode.OK);
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg.JWTConfig.Secret));

        var token = new JwtSecurityToken(
            issuer: _cfg.JWTConfig.ValidIssuer,
            audience: _cfg.JWTConfig.ValidAudience,
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}