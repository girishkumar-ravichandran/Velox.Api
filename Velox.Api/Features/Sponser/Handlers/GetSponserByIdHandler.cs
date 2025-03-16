using MediatR;
using Velox.Api.Features.Sponser.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Handlers
{
    public class GetSponserByIdHandler : IRequestHandler<GetSponserByIdQuery, SponserDTO>
    {
        private readonly ISponserServiceDAO _SponserServiceDAO;

        public GetSponserByIdHandler(ISponserServiceDAO SponserRepository)
        {
            _SponserServiceDAO = SponserRepository;
        }

        public async Task<SponserDTO> Handle(GetSponserByIdQuery request, CancellationToken cancellationToken)
        {
            var Sponser = await _SponserServiceDAO.GetSponserByIdAsync(request.SponserId);

            if (Sponser == null)
                return null;

            return new SponserDTO
            {
                Id = Sponser.Id,
                Name = Sponser.Name,
                StartDate = Sponser.StartDate,
                EndDate = Sponser.EndDate,
                Location = Sponser.Location
            };
        }
    }
}
