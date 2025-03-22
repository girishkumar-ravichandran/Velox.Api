
namespace Velox.Api.Middleware.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string Email);
    }
}
