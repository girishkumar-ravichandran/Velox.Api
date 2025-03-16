using MediatR;
using Velox.Api.Features.Panelist.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Panelist.Handlers
{
    public class CreatePanelistHandler : IRequestHandler<CreatePanelistCommand, PanelistDTO>
    {
        private readonly IPanelistServiceDAO _PanelistServiceDAO;

        public CreatePanelistHandler(IPanelistServiceDAO PanelistRepository)
        {
            _PanelistServiceDAO = PanelistRepository;
        }

        public async Task<PanelistDTO> Handle(CreatePanelistCommand request, CancellationToken cancellationToken)
        {
            var Panelist = new PanelistDTO
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location
            };

            var createdPanelist = await _PanelistServiceDAO.RegisterPanelist(Panelist);

            return new PanelistDTO
            {
                Id = createdPanelist.Id,
                Name = createdPanelist.Name,
                StartDate = createdPanelist.StartDate,
                EndDate = createdPanelist.EndDate,
                Location = createdPanelist.Location
            };
        }
    }
}
