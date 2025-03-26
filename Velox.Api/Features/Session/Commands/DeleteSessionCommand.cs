using MediatR;

namespace Velox.Api.Features.Session.Commands
{
    public class DeleteSessionCommand : IRequest<(bool isSuccess, string message)>
    {
        public string SessionId { get; set; }

        public DeleteSessionCommand(string sessionId)
        {
            SessionId = sessionId;
        }
    }
}
