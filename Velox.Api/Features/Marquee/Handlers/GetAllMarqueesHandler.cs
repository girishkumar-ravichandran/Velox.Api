using MediatR;
using Velox.Api.Features.Marquee.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Marquee.Handlers
{
    public class GetAllMarqueeHandler : IRequestHandler<GetAllMarqueesQuery, IEnumerable<MarqueeDTO>>
    {
        private readonly IMarqueeServiceDAO _MarqueeServiceDAO;

        public GetAllMarqueeHandler(IMarqueeServiceDAO MarqueeRepository)
        {
            _MarqueeServiceDAO = MarqueeRepository;
        }

        public async Task<IEnumerable<MarqueeDTO>> Handle(GetAllMarqueesQuery request, CancellationToken cancellationToken)
        {
            var Marquees = await _MarqueeServiceDAO.GetAllMarqueesAsync();

            var MarqueeDtos = new List<MarqueeDTO>();
            foreach (var Marquee in Marquees)
            {
                MarqueeDtos.Add(new MarqueeDTO
                {
                    Id = Marquee.Id,
                    Name = Marquee.Name,
                    StartDate = Marquee.StartDate,
                    EndDate = Marquee.EndDate,
                    Location = Marquee.Location
                });
            }

            return MarqueeDtos;
        }
    }
}
