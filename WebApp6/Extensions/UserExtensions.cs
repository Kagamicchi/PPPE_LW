using WebApp6.Models;
using WebApp6.Models.Dto.User;

namespace WebApp6.Extensions
{
    public static class UserExtensions
    {
        public static UserModel ToModel(this RegisterUserRequest request)
        {
            return new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthday = request.Birthday,
                Email = request.Email
            };
        }

        public static UserResponse ToResponse(this UserModel user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                LastAuth = user.LastAuth
            };
        }
    }
}
