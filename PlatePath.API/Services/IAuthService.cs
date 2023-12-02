using PlatePath.API.Data.Models.Authentication.Login;
using PlatePath.API.Data.Models.Authentication.SignUp;

namespace PlatePath.API.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);

        Task<RegisterResponse> Register(RegisterRequest request);
    }
}
