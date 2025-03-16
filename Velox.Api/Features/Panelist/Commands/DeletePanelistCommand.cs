using MediatR;

namespace Velox.Api.Features.Panelist.Commands
{
    public class DeletePanelistCommand : IRequest<bool>
    {
        public int PanelistId { get; set; }
    }
}
