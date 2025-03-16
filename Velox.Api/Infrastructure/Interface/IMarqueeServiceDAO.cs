using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Infrastructure.Interface
{
    public interface IMarqueeServiceDAO
    {
        Task<MarqueeDTO> RegisterMarquee(MarqueeDTO Marquee);

        Task<MarqueeDTO> GetMarqueeByIdAsync(int MarqueeId);

        Task<IEnumerable<MarqueeDTO>> GetAllMarqueesAsync();

        Task<MarqueeDTO> UpdateMarquee(MarqueeDTO Marquee);

        Task DeleteMarquee(int MarqueeId);
    }
}
