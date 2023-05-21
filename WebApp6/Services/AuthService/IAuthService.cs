using WebApp6.Models.Dto.User;
using WebApp6.Models.Dto;

namespace WebApp6.Services.AuthService
{
    public interface IAuthService
    {
        Task<BaseResponse<AuthResponse>> Login(LoginUserRequest request);
        Task<BaseResponse<UserResponse>> Register(RegisterUserRequest request);
    }
}
