using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Marquee.Queries
{
    public class GetMarqueeByIdQuery : IRequest<MarqueeDTO>
    {
        public int MarqueeId { get; set; }
    }
}
