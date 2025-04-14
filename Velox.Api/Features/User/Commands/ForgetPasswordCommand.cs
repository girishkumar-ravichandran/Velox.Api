using MediatR;
using Velox.Api.Infrastructure.DTO.ResponseDTO;

namespace Velox.Api.Features.User.Commands
{
    public class ForgetPasswordCommand : IRequest<ForgetPasswordResponseDTO>
    {
        public string Email { get; set; }
        public string NewPasswordHash { get; set; }
        public string NewSalt { get; set; }
    }
}
