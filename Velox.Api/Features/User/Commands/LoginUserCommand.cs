using MediatR;
using Velox.Api.Infrastructure.DTO.ResponseDTO;

namespace Velox.Api.Features.User.Commands
{
    public class LoginUserCommand : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
