using MediatR;
using Velox.Api.Infrastructure.DTO.ResponseDTO;

namespace Velox.Api.Features.User.Commands
{
    public class GetUserOTPCommand : IRequest<GetUserOTPResponseDTO>
    {
        public string Username { get; set; }
    }
}
