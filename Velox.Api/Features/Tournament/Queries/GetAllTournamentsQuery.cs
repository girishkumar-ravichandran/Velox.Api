using MediatR;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Tournament.Queries
{
    public class GetAllTournamentsQuery : IRequest<IEnumerable<TournamentDTO>>
    {
    }
}
