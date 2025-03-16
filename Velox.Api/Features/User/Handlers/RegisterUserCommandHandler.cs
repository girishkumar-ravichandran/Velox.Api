using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Enum;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Features.User.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDTO>
    {
        private readonly IUserServiceDAO _userServiceDAO;
        private readonly IPasswordService _passwordService;

        public RegisterUserCommandHandler(IUserServiceDAO userServiceDAO, IPasswordService passwordService)
        {
            _userServiceDAO = userServiceDAO;
            _passwordService = passwordService;
        }

        public async Task<UserDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var salt = _passwordService.GenerateSalt(request.Email);
            var passwordHash = _passwordService.HashPassword(request.PasswordHash, salt);
            var RoleEnum = request.Role.ToString();
            var GenderEnum = request.Gender.ToString();

            var user = new UserDTO
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                Gender = GenderEnum,
                Role = RoleEnum,
                DOB = request.DOB,
                PasswordSalt = salt

            };

            await _userServiceDAO.RegisterUserAsync(user);

            return user;
        }
    }
}
