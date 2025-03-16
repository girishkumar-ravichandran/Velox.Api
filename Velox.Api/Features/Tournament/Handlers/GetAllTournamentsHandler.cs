using MediatR;
using Velox.Api.Features.Tournament.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Tournament.Handlers
{
    public class GetAllTournamentHandler : IRequestHandler<GetAllTournamentsQuery, IEnumerable<TournamentDTO>>
    {
        private readonly ITournamentServiceDAO _tournamentServiceDAO;

        public GetAllTournamentHandler(ITournamentServiceDAO tournamentRepository)
        {
            _tournamentServiceDAO = tournamentRepository;
        }

        public async Task<IEnumerable<TournamentDTO>> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = await _tournamentServiceDAO.GetAllTournamentsAsync();

            var tournamentDtos = new List<TournamentDTO>();
            foreach (var tournament in tournaments)
            {
                tournamentDtos.Add(new TournamentDTO
                {
                    Id = tournament.Id,
                    TournamentName = tournament.TournamentName,
                    StartDate = tournament.StartDate,
                    EndDate = tournament.EndDate,
                    Location = tournament.Location,
                    Description = tournament.Description,
                });
            }

            return tournamentDtos;
        }
    }
}
