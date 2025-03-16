using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Commands
{
    public class UpdateSponserCommand : IRequest<SponserDTO>
    {
        public int SponserId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
    }
}
