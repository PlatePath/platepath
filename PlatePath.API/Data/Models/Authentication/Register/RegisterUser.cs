using PlatePath.API.Singleton;
using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Data.Models.Authentication.SignUp
{
    public record RegisterRequest
    {
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public int? Age { get; set; }

        public double? HeightCm { get; set; }

        public double? WeightKg { get; set; }

        public int? ActivityLevel { get; set; }

        public int? Gender { get; set; }

        public int? WeightGoal { get; set; }
    }

    public record RegisterResponse : BaseResponse
    {
        public RegisterResponse() { }
        public RegisterResponse(ErrorCode error) : base(error) { }
    }
};