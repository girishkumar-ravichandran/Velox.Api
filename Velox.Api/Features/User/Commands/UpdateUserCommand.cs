using Velox.Api.Infrastructure.DTO;
using MediatR;

namespace Velox.Api.Features.User.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender {  get; set; } 
        public DateTime DOB { get; set; }

    }
}
