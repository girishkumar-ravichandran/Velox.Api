using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Queries
{
    public class GetAllSponsersQuery : IRequest<IEnumerable<SponserDTO>>
    {
    }
}
