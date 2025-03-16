using MediatR;
using Velox.Api.Features.Sponser.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Sponser.Handlers
{
    public class GetAllSponserHandler : IRequestHandler<GetAllSponsersQuery, IEnumerable<SponserDTO>>
    {
        private readonly ISponserServiceDAO _SponserServiceDAO;

        public GetAllSponserHandler(ISponserServiceDAO SponserRepository)
        {
            _SponserServiceDAO = SponserRepository;
        }

        public async Task<IEnumerable<SponserDTO>> Handle(GetAllSponsersQuery request, CancellationToken cancellationToken)
        {
            var Sponsers = await _SponserServiceDAO.GetAllSponsersAsync();

            var SponserDtos = new List<SponserDTO>();
            foreach (var Sponser in Sponsers)
            {
                SponserDtos.Add(new SponserDTO
                {
                    Id = Sponser.Id,
                    Name = Sponser.Name,
                    StartDate = Sponser.StartDate,
                    EndDate = Sponser.EndDate,
                    Location = Sponser.Location
                });
            }

            return SponserDtos;
        }
    }
}
