using MediatR;

namespace Velox.Api.Features.Session.Commands
{
    public class CreateSessionCommand : IRequest<(bool isSuccess, string sessionId, string message)>
    {
        public int UserId { get; set; }
        public string SessionData { get; set; }

        public CreateSessionCommand(int userId, string sessionData)
        {
            UserId = userId;
            SessionData = sessionData;
        }
    }
}
