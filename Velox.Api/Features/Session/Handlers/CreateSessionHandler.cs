using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Session.Commands
{
    public class CreateSessionHandler : IRequestHandler<CreateSessionCommand, (bool isSuccess, string sessionId, string message)>
    {
        private readonly ISessionServiceDAO _sessionServiceDAO;

        public CreateSessionHandler(ISessionServiceDAO sessionServiceDAO)
        {
            _sessionServiceDAO = sessionServiceDAO;
        }

        public async Task<(bool isSuccess, string sessionId, string message)> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            return await _sessionServiceDAO.CreateSessionAsync(request.UserId, request.SessionData);
        }
    }
}
