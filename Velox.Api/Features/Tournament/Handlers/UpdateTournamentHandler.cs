using MediatR;
using Velox.Api.Features.Tournament.Commands;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Tournament.Handlers
{
    public class UpdateTournamentHandler : IRequestHandler<UpdateTournamentCommand, TournamentDTO>
    {
        private readonly ITournamentServiceDAO _tournamentServiceDAO;

        public UpdateTournamentHandler(ITournamentServiceDAO tournamentRepository)
        {
            _tournamentServiceDAO = tournamentRepository;
        }

        public async Task<TournamentDTO> Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentServiceDAO.GetTournamentByIdAsync(request.TournamentId);

            if (tournament == null)
                return null;

            tournament.TournamentName = request.TournamentName;
            tournament.StartDate = request.StartDate;
            tournament.EndDate = request.EndDate;
            tournament.Location = request.Location;
            tournament.Description = request.Description;


            var updatedTournament = await _tournamentServiceDAO.UpdateTournament(tournament);

            return new TournamentDTO
            {
                Id = updatedTournament.Id,
                TournamentName = updatedTournament.TournamentName,
                StartDate = updatedTournament.StartDate,
                EndDate = updatedTournament.EndDate,
                Location = updatedTournament.Location,
                Description = tournament.Description
            };
        }
    }
}
