using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Queries
{
    public class GetSponserByIdQuery : IRequest<SponserDTO>
    {
        public int SponserId { get; set; }
    }
}
