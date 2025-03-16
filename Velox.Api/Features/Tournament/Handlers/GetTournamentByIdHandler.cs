using MediatR;
using Velox.Api.Features.Tournament.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Tournament.Handlers
{
    public class GetTournamentByIdHandler : IRequestHandler<GetTournamentByIdQuery, TournamentDTO>
    {
        private readonly ITournamentServiceDAO _tournamentServiceDAO;

        public GetTournamentByIdHandler(ITournamentServiceDAO tournamentRepository)
        {
            _tournamentServiceDAO = tournamentRepository;
        }

        public async Task<TournamentDTO> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentServiceDAO.GetTournamentByIdAsync(request.TournamentId);

            if (tournament == null)
                return null;

            return new TournamentDTO
            {
                Id = tournament.Id,
                TournamentName = tournament.TournamentName,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Location = tournament.Location,
                Description = tournament.Description,
            };
        }
    }
}
