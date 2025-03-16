using MediatR;
using Velox.Api.Features.Panelist.Commands;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Infrastructure.DTO;

namespace Velox.Api.Features.Panelist.Handlers
{
    public class UpdatePanelistHandler : IRequestHandler<UpdatePanelistCommand, PanelistDTO>
    {
        private readonly IPanelistServiceDAO _PanelistServiceDAO;

        public UpdatePanelistHandler(IPanelistServiceDAO PanelistRepository)
        {
            _PanelistServiceDAO = PanelistRepository;
        }

        public async Task<PanelistDTO> Handle(UpdatePanelistCommand request, CancellationToken cancellationToken)
        {
            var Panelist = await _PanelistServiceDAO.GetPanelistByIdAsync(request.PanelistId);

            if (Panelist == null)
                return null;

            Panelist.Name = request.Name;
            Panelist.StartDate = request.StartDate;
            Panelist.EndDate = request.EndDate;
            Panelist.Location = request.Location;

            var updatedPanelist = await _PanelistServiceDAO.UpdatePanelist(Panelist);

            return new PanelistDTO
            {
                Id = updatedPanelist.Id,
                Name = updatedPanelist.Name,
                StartDate = updatedPanelist.StartDate,
                EndDate = updatedPanelist.EndDate,
                Location = updatedPanelist.Location
            };
        }
    }
}
