using MediatR;
using Velox.Api.Features.Marquee.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Marquee.Handlers
{
    public class CreateMarqueeHandler : IRequestHandler<CreateMarqueeCommand, MarqueeDTO>
    {
        private readonly IMarqueeServiceDAO _MarqueeServiceDAO;

        public CreateMarqueeHandler(IMarqueeServiceDAO MarqueeRepository)
        {
            _MarqueeServiceDAO = MarqueeRepository;
        }

        public async Task<MarqueeDTO> Handle(CreateMarqueeCommand request, CancellationToken cancellationToken)
        {
            var Marquee = new MarqueeDTO
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location
            };

            var createdMarquee = await _MarqueeServiceDAO.RegisterMarquee(Marquee);

            return new MarqueeDTO
            {
                Id = createdMarquee.Id,
                Name = createdMarquee.Name,
                StartDate = createdMarquee.StartDate,
                EndDate = createdMarquee.EndDate,
                Location = createdMarquee.Location
            };
        }
    }
}
