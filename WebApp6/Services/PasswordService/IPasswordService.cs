using WebApp6.Models;

namespace WebApp6.Services.PasswordService
{
    public interface IPasswordService
    {
        void SetUserPasswordHash(UserModel user, string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}