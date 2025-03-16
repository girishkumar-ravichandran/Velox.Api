using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Middleware.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string Email);
    }
}
