using MediatR;
using Velox.Api.Features.Marquee.Commands;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Marquee.Handlers
{
    public class DeleteMarqueeHandler : IRequestHandler<DeleteMarqueeCommand, bool>
    {
        private readonly IMarqueeServiceDAO _MarqueeServiceDAO;

        public DeleteMarqueeHandler(IMarqueeServiceDAO MarqueeRepository)
        {
            _MarqueeServiceDAO = MarqueeRepository;
        }

        public async Task<bool> Handle(DeleteMarqueeCommand request, CancellationToken cancellationToken)
        {
            var Marquee = await _MarqueeServiceDAO.GetMarqueeByIdAsync(request.MarqueeId);

            if (Marquee == null)
                return false;

            await _MarqueeServiceDAO.DeleteMarquee(request.MarqueeId);
            return true;
        }
    }
}
