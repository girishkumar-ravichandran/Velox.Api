using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Queries
{
    public class GetPanelistByIdQuery : IRequest<PanelistDTO>
    {
        public int PanelistId { get; set; }
    }
}
