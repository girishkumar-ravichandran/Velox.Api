using System.Threading.Tasks;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Infrastructure.Interface
{
    public interface ISessionServiceDAO
    {
        Task<(bool isSuccess, string sessionId, string message)> CreateSessionAsync(int userId, string sessionData);
        Task<SessionDTO> GetSessionByIdAsync(string sessionId);
        Task<(bool isSuccess, string message)> DeleteSessionAsync(string sessionId);
        Task<int> CleanupExpiredSessionsAsync();
    }
}
