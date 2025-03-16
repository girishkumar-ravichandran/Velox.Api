using MediatR;
using Velox.Api.Features.Sponser.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Sponser.Handlers
{
    public class CreateSponserHandler : IRequestHandler<CreateSponserCommand, SponserDTO>
    {
        private readonly ISponserServiceDAO _SponserServiceDAO;

        public CreateSponserHandler(ISponserServiceDAO SponserRepository)
        {
            _SponserServiceDAO = SponserRepository;
        }

        public async Task<SponserDTO> Handle(CreateSponserCommand request, CancellationToken cancellationToken)
        {
            var Sponser = new SponserDTO
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location
            };

            var createdSponser = await _SponserServiceDAO.RegisterSponser(Sponser);

            return new SponserDTO
            {
                Id = createdSponser.Id,
                Name = createdSponser.Name,
                StartDate = createdSponser.StartDate,
                EndDate = createdSponser.EndDate,
                Location = createdSponser.Location
            };
        }
    }
}
