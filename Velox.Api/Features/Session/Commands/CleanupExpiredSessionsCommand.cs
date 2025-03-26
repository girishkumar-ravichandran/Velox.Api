using MediatR;

namespace Velox.Api.Features.Session.Commands
{
    public class CleanupExpiredSessionsCommand : IRequest<int>
    {
    }
}
