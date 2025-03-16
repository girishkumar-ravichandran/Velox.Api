using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Infrastructure.Interface
{
    public interface ISponserServiceDAO
    {
        Task<SponserDTO> RegisterSponser(SponserDTO Sponser);

        Task<SponserDTO> GetSponserByIdAsync(int SponserId);

        Task<IEnumerable<SponserDTO>> GetAllSponsersAsync();

        Task<SponserDTO> UpdateSponser(SponserDTO Sponser);

        Task DeleteSponser(int SponserId);
    }
}
