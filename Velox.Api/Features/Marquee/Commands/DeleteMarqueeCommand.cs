using MediatR;

namespace Velox.Api.Features.Marquee.Commands
{
    public class DeleteMarqueeCommand : IRequest<bool>
    {
        public int MarqueeId { get; set; }
    }
}
