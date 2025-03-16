using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Queries
{
    public class GetAllPanelistsQuery : IRequest<IEnumerable<PanelistDTO>>
    {
    }
}
