using MediatR;

namespace Velox.Api.Features.Sponser.Commands
{
    public class DeleteSponserCommand : IRequest<bool>
    {
        public int SponserId { get; set; }
    }
}
