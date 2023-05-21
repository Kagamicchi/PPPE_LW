using System.ComponentModel.DataAnnotations;

namespace WebApp6.Models.Dto.User
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public DateOnly LastAuth { get; set; }
    }
}
