using PlatePath.API.Singleton;

namespace PlatePath.API.Data.Models.Authentication.Login
{
    public record LoginRequest
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public record LoginResponse : BaseResponse
    {
        public LoginResponse() { }
        public LoginResponse(ErrorCode error) : base(error) { }

        public string? UserId { get; set; }

        public string? Token { get; set; }

        public DateTime? Expiration { get; set; }
    }
}
