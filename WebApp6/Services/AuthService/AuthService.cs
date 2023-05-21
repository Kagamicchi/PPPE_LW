using System.Security.Claims;
using WebApp6.Models.Dto.User;
using WebApp6.Models.Dto;
using WebApp6.Services.PasswordService;
using WebApp6.Models;
using WebApp6.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp6.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private List<UserModel> _users;

        public AuthService(IConfiguration configuration, IPasswordService passwordService)
        {
            _configuration = configuration;
            _passwordService = passwordService;

            _users = new List<UserModel>
            {
                new UserModel
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Helen",
                    LastName = "Sherstiuk",
                    Email = "helen@example.com",
                    Birthday = new DateOnly(2003, 7, 29),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 0,
                    IsLocked = false
                },
                new UserModel
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Maxim",
                    LastName = "Kutuzaki",
                    Email = "max.kut@example.com",
                    Birthday = new DateOnly(2001, 1, 3),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 2,
                    IsLocked = false
                },
                new UserModel
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "Alina",
                    LastName = "Melnik",
                    Email = "melnik.alinka@example.com",
                    Birthday = new DateOnly(2002, 5, 31),
                    LastAuth = DateOnly.FromDateTime(DateTime.Now),
                    AuthFailedCount = 1,
                    IsLocked = false
                }
            };
            _passwordService.SetUserPasswordHash(_users[0], "lena29072003");
            _passwordService.SetUserPasswordHash(_users[1], "cool99");
            _passwordService.SetUserPasswordHash(_users[2], "iloveanime");
        }

        public async Task<BaseResponse<AuthResponse>> Login(LoginUserRequest request)
        {
            try
            {
                var failResponse = new BaseResponse<AuthResponse>
                {
                    Message = "Authentication failed",
                    ValueCount = 1,
                    Values = new List<AuthResponse> { new AuthResponse() { Message = "Authentication failed" } }
                };

                if (_users.Count == 0)
                {
                    return failResponse;
                }
                var user = await Task.FromResult(_users.Find(x => x.Email.Equals(request.Email)));
                if (user == null)
                {
                    return failResponse;
                }

                if (!_passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    user.AuthFailedCount++;
                    bool isLockThresholdParsed = int.TryParse(
                            _configuration.GetSection("Authorization:LockThreshold").Value,
                            out var lockThreshold
                        );
                    if (isLockThresholdParsed && user.AuthFailedCount >= lockThreshold) { user.IsLocked = true; }
                    return failResponse;
                }

                user.LastAuth = DateOnly.FromDateTime(DateTime.Now);
                string token = CreateToken(user);

                var response = new AuthResponse
                {
                    Success = true,
                    UserName = $"{user.FirstName} {user.LastName}",
                    Token = token,
                };
                return new BaseResponse<AuthResponse>
                {
                    Success = response.Success,
                    Message = "Success in auth",
                    StatusCode = StatusCodes.Status200OK,
                    ValueCount = 1,
                    Values = new List<AuthResponse> { response }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AuthResponse>()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponse<UserResponse>> Register(RegisterUserRequest request)
        {
            try
            {
                if (await Task.Run(() => _users.Find(x => request.Email.Equals(x.Email))) != null)
                {
                    return new BaseResponse<UserResponse>()
                    {
                        Message = "The email address is already in use",
                        StatusCode = StatusCodes.Status409Conflict,
                    };
                }

                var user = request.ToModel();
                _passwordService.SetUserPasswordHash(user, request.Password);
                user.UserId = Guid.NewGuid();

                _users.Add(user);
                return new BaseResponse<UserResponse>()
                {
                    Message = "User registered",
                    StatusCode = StatusCodes.Status201Created,
                    ValueCount = 1,
                    Values = new List<UserResponse> { user.ToResponse() }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserResponse>()
                {
                    Message = ex.Message
                };
            }
        }

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, value: $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Authorization:TokenKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
