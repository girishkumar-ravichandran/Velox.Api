using MediatR;
using Velox.Api.Features.Marquee.Commands;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Marquee.Handlers
{
    public class UpdateMarqueeHandler : IRequestHandler<UpdateMarqueeCommand, MarqueeDTO>
    {
        private readonly IMarqueeServiceDAO _MarqueeServiceDAO;

        public UpdateMarqueeHandler(IMarqueeServiceDAO MarqueeRepository)
        {
            _MarqueeServiceDAO = MarqueeRepository;
        }

        public async Task<MarqueeDTO> Handle(UpdateMarqueeCommand request, CancellationToken cancellationToken)
        {
            var Marquee = await _MarqueeServiceDAO.GetMarqueeByIdAsync(request.MarqueeId);

            if (Marquee == null)
                return null;

            Marquee.Name = request.Name;
            Marquee.StartDate = request.StartDate;
            Marquee.EndDate = request.EndDate;
            Marquee.Location = request.Location;

            var updatedMarquee = await _MarqueeServiceDAO.UpdateMarquee(Marquee);

            return new MarqueeDTO
            {
                Id = updatedMarquee.Id,
                Name = updatedMarquee.Name,
                StartDate = updatedMarquee.StartDate,
                EndDate = updatedMarquee.EndDate,
                Location = updatedMarquee.Location
            };
        }
    }
}
