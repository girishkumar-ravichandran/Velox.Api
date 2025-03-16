using MediatR;
using Velox.Api.Infrastructure.DTO;


namespace Velox.Api.Features.Marquee.Commands
{
    public class CreateMarqueeCommand : IRequest<MarqueeDTO>
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
    }
}
