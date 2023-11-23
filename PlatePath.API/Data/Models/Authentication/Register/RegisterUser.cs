using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Data.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public int ActivityLevel { get; set; }

        public int Gender { get; set; }

        public int WeightGoal { get; set; }
    }
}
