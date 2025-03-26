using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Session.Commands
{
    public class DeleteSessionHandler : IRequestHandler<DeleteSessionCommand, (bool isSuccess, string message)>
    {
        private readonly ISessionServiceDAO _sessionServiceDAO;

        public DeleteSessionHandler(ISessionServiceDAO sessionServiceDAO)
        {
            _sessionServiceDAO = sessionServiceDAO;
        }

        public async Task<(bool isSuccess, string message)> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            return await _sessionServiceDAO.DeleteSessionAsync(request.SessionId);
        }
    }
}
