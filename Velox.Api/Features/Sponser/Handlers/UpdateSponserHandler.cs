using MediatR;
using Velox.Api.Features.Sponser.Commands;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Handlers
{
    public class UpdateSponserHandler : IRequestHandler<UpdateSponserCommand, SponserDTO>
    {
        private readonly ISponserServiceDAO _SponserServiceDAO;

        public UpdateSponserHandler(ISponserServiceDAO SponserRepository)
        {
            _SponserServiceDAO = SponserRepository;
        }

        public async Task<SponserDTO> Handle(UpdateSponserCommand request, CancellationToken cancellationToken)
        {
            var Sponser = await _SponserServiceDAO.GetSponserByIdAsync(request.SponserId);

            if (Sponser == null)
                return null;

            Sponser.Name = request.Name;
            Sponser.StartDate = request.StartDate;
            Sponser.EndDate = request.EndDate;
            Sponser.Location = request.Location;

            var updatedSponser = await _SponserServiceDAO.UpdateSponser(Sponser);

            return new SponserDTO
            {
                Id = updatedSponser.Id,
                Name = updatedSponser.Name,
                StartDate = updatedSponser.StartDate,
                EndDate = updatedSponser.EndDate,
                Location = updatedSponser.Location
            };
        }
    }
}
