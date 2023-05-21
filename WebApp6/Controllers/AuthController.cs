using Microsoft.AspNetCore.Mvc;
using WebApp6.Models.Dto;
using WebApp6.Models.Dto.User;
using WebApp6.Services.AuthService;

namespace WebApp6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseResponse<UserResponse>>> Register(RegisterUserRequest request)
        {
            var response = await _authService.Register(request);
            return response.StatusCode switch
            {
                StatusCodes.Status201Created => Ok(response),
                StatusCodes.Status409Conflict => Conflict(response),
                _ => BadRequest(response),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<AuthResponse>>> Login(LoginUserRequest request)
        {
            var response = await _authService.Login(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
