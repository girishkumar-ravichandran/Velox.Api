using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Tournament.Queries
{
    public class GetTournamentByIdQuery : IRequest<TournamentDTO>
    {
        public int TournamentId { get; set; }
    }
}
