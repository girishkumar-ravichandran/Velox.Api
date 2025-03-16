using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Commands
{
    public class UpdatePanelistCommand : IRequest<PanelistDTO>
    {
        public int PanelistId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
    }
}
