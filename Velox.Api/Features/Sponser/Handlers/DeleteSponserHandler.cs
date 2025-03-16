using MediatR;
using Velox.Api.Features.Sponser.Commands;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Sponser.Handlers
{
    public class DeleteSponserHandler : IRequestHandler<DeleteSponserCommand, bool>
    {
        private readonly ISponserServiceDAO _SponserServiceDAO;

        public DeleteSponserHandler(ISponserServiceDAO SponserRepository)
        {
            _SponserServiceDAO = SponserRepository;
        }

        public async Task<bool> Handle(DeleteSponserCommand request, CancellationToken cancellationToken)
        {
            var Sponser = await _SponserServiceDAO.GetSponserByIdAsync(request.SponserId);

            if (Sponser == null)
                return false;

            await _SponserServiceDAO.DeleteSponser(request.SponserId);
            return true;
        }
    }
}
