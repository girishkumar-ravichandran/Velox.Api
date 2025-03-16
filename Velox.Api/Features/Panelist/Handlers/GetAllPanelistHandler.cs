using MediatR;
using Velox.Api.Features.Panelist.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Handlers
{
    public class GetAllPanelistHandler : IRequestHandler<GetAllPanelistsQuery, IEnumerable<PanelistDTO>>
    {
        private readonly IPanelistServiceDAO _PanelistServiceDAO;

        public GetAllPanelistHandler(IPanelistServiceDAO PanelistRepository)
        {
            _PanelistServiceDAO = PanelistRepository;
        }

        public async Task<IEnumerable<PanelistDTO>> Handle(GetAllPanelistsQuery request, CancellationToken cancellationToken)
        {
            var Panelists = await _PanelistServiceDAO.GetAllPanelistsAsync();

            var PanelistDtos = new List<PanelistDTO>();
            foreach (var Panelist in Panelists)
            {
                PanelistDtos.Add(new PanelistDTO
                {
                    Id = Panelist.Id,
                    Name = Panelist.Name,
                    StartDate = Panelist.StartDate,
                    EndDate = Panelist.EndDate,
                    Location = Panelist.Location
                });
            }

            return PanelistDtos;
        }
    }
}
