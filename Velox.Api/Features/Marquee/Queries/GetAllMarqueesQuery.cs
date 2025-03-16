using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Marquee.Queries
{
    public class GetAllMarqueesQuery : IRequest<IEnumerable<MarqueeDTO>>
    {
    }
}
