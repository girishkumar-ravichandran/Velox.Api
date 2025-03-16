using MediatR;
using Velox.Api.Features.Panelist.Commands;
using Velox.Api.Infrastructure.Interface;

namespace Velox.Api.Features.Panelist.Handlers
{
    public class DeletePanelistHandler : IRequestHandler<DeletePanelistCommand, bool>
    {
        private readonly IPanelistServiceDAO _PanelistServiceDAO;

        public DeletePanelistHandler(IPanelistServiceDAO PanelistRepository)
        {
            _PanelistServiceDAO = PanelistRepository;
        }

        public async Task<bool> Handle(DeletePanelistCommand request, CancellationToken cancellationToken)
        {
            var Panelist = await _PanelistServiceDAO.GetPanelistByIdAsync(request.PanelistId);

            if (Panelist == null)
                return false;

            await _PanelistServiceDAO.DeletePanelist(request.PanelistId);
            return true;
        }
    }
}
