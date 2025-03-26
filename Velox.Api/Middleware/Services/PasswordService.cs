using System.Security.Cryptography;
using System.Text;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Middleware.Services
{
    public class PasswordService:IPasswordService
    {
        public string GenerateSalt(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty.");

            string rawSalt = $"velox{email}sports";
            return ComputeSHA256(rawSalt);
        }

        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string combined = password + salt;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            string computedHash = HashPassword(enteredPassword, salt);
            return computedHash == storedHash;
        }

        private string ComputeSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
