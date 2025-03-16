using MediatR;
using Velox.Api.Features.Panelist.Queries;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Handlers
{
    public class GetPanelistByIdHandler : IRequestHandler<GetPanelistByIdQuery, PanelistDTO>
    {
        private readonly IPanelistServiceDAO _PanelistServiceDAO;

        public GetPanelistByIdHandler(IPanelistServiceDAO PanelistRepository)
        {
            _PanelistServiceDAO = PanelistRepository;
        }

        public async Task<PanelistDTO> Handle(GetPanelistByIdQuery request, CancellationToken cancellationToken)
        {
            var Panelist = await _PanelistServiceDAO.GetPanelistByIdAsync(request.PanelistId);

            if (Panelist == null)
                return null;

            return new PanelistDTO
            {
                Id = Panelist.Id,
                Name = Panelist.Name,
                StartDate = Panelist.StartDate,
                EndDate = Panelist.EndDate,
                Location = Panelist.Location
            };
        }
    }
}
