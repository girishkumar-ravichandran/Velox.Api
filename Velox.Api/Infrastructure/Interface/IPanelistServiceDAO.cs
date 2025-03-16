using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Infrastructure.Interface
{
    public interface IPanelistServiceDAO
    {
        Task<PanelistDTO> RegisterPanelist(PanelistDTO Panelist);

        Task<PanelistDTO> GetPanelistByIdAsync(int PanelistId);

        Task<IEnumerable<PanelistDTO>> GetAllPanelistsAsync();

        Task<PanelistDTO> UpdatePanelist(PanelistDTO Panelist);

        Task DeletePanelist(int PanelistId);
    }
}
