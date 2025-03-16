using MediatR;
using Velox.Api.Features.Tournament.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Tournament.Handlers
{
    public class CreateTournamentHandler : IRequestHandler<CreateTournamentCommand, TournamentDTO>
    {
        private readonly ITournamentServiceDAO _tournamentServiceDAO;

        public CreateTournamentHandler(ITournamentServiceDAO tournamentRepository)
        {
            _tournamentServiceDAO = tournamentRepository;
        }

        public async Task<TournamentDTO> Handle(CreateTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = new TournamentDTO
            {
                TournamentName = request.TournamentName,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location,
                Description = request.Description

            };

            var createdTournament = await _tournamentServiceDAO.RegisterTournament(tournament);

            return new TournamentDTO
            {
                Id = createdTournament.Id,
                TournamentName = createdTournament.TournamentName,
                StartDate = createdTournament.StartDate,
                EndDate = createdTournament.EndDate,
                Location = createdTournament.Location,
                Description = createdTournament.Description
            };
        }
    }
}
