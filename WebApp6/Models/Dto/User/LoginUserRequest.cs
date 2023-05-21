using System.ComponentModel.DataAnnotations;

namespace WebApp6.Models.Dto.User
{
    public class LoginUserRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
