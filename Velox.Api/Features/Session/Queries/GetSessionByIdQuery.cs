using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Session.Queries
{
    public class GetSessionByIdQuery : IRequest<SessionDTO>
    {
        public string SessionId { get; set; }
    }
}
