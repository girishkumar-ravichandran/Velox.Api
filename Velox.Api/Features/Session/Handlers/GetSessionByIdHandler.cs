using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Session.Queries
{
    public class GetSessionByIdHandler : IRequestHandler<GetSessionByIdQuery, SessionDTO>
    {
        private readonly ISessionServiceDAO _sessionServiceDAO;

        public GetSessionByIdHandler(ISessionServiceDAO sessionServiceDAO)
        {
            _sessionServiceDAO = sessionServiceDAO;
        }

        public async Task<SessionDTO> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            var session = await _sessionServiceDAO.GetSessionByIdAsync(request.SessionId);
            return session ?? throw new Exception("Session not found");
        }
    }
}
