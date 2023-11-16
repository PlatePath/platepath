using System.ComponentModel.DataAnnotations;

namespace PlatePath.API.Data.Models.Authentication.Login
{
    public class LoginUser
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
