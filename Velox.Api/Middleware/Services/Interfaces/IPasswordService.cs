namespace Velox.Api.Middleware.Services.Interfaces
{
    public interface IPasswordService
    {
        string GenerateSalt(string email);
        string HashPassword(string password, string salt);
        bool VerifyPassword(string enteredPassword, string storedHash, string salt);
    }
}
