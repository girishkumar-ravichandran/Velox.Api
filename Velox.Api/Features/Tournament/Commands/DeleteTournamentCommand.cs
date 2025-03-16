using MediatR;

namespace Velox.Api.Features.Tournament.Commands
{
    public class DeleteTournamentCommand : IRequest<bool>
    {
        public int TournamentId { get; set; }
    }
}
