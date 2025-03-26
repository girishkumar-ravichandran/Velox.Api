using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Session.Commands
{
    public class CleanupExpiredSessionsHandler : IRequestHandler<CleanupExpiredSessionsCommand, int>
    {
        private readonly ISessionServiceDAO _sessionServiceDAO;

        public CleanupExpiredSessionsHandler(ISessionServiceDAO sessionServiceDAO)
        {
            _sessionServiceDAO = sessionServiceDAO;
        }

        public async Task<int> Handle(CleanupExpiredSessionsCommand request, CancellationToken cancellationToken)
        {
            return await _sessionServiceDAO.CleanupExpiredSessionsAsync();
        }
    }
}
