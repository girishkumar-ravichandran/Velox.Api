using MediatR;
using Velox.Api.Features.Tournament.Commands;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Tournament.Handlers
{
    public class DeleteTournamentHandler : IRequestHandler<DeleteTournamentCommand, bool>
    {
        private readonly ITournamentServiceDAO _tournamentServiceDAO;

        public DeleteTournamentHandler(ITournamentServiceDAO tournamentRepository)
        {
            _tournamentServiceDAO = tournamentRepository;
        }

        public async Task<bool> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentServiceDAO.GetTournamentByIdAsync(request.TournamentId);

            if (tournament == null)
                return false;

            await _tournamentServiceDAO.DeleteTournament(request.TournamentId);
            return true;
        }
    }
}
