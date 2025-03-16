using MediatR;
using Velox.Api.Features.Marquee.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Marquee.Handlers
{
    public class GetMarqueeByIdHandler : IRequestHandler<GetMarqueeByIdQuery, MarqueeDTO>
    {
        private readonly IMarqueeServiceDAO _MarqueeServiceDAO;

        public GetMarqueeByIdHandler(IMarqueeServiceDAO MarqueeRepository)
        {
            _MarqueeServiceDAO = MarqueeRepository;
        }

        public async Task<MarqueeDTO> Handle(GetMarqueeByIdQuery request, CancellationToken cancellationToken)
        {
            var Marquee = await _MarqueeServiceDAO.GetMarqueeByIdAsync(request.MarqueeId);

            if (Marquee == null)
                return null;

            return new MarqueeDTO
            {
                Id = Marquee.Id,
                Name = Marquee.Name,
                StartDate = Marquee.StartDate,
                EndDate = Marquee.EndDate,
                Location = Marquee.Location
            };
        }
    }
}
