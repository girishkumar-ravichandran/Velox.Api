using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Infrastructure.Interface
{
    public interface ITournamentServiceDAO
    {
        Task<TournamentDTO> RegisterTournament(TournamentDTO tournament);

        Task<TournamentDTO> GetTournamentByIdAsync(int tournamentId);

        Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync();

        Task<TournamentDTO> UpdateTournament(TournamentDTO tournament);

        Task DeleteTournament(int tournamentId);
    }
}
