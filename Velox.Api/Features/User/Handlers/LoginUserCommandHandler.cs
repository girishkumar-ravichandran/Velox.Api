using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Features.User.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDTO>
    {
        private readonly IUserServiceDAO _userServiceDAO;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUserServiceDAO userServiceDAO, ITokenService tokenService)
        {
            _userServiceDAO = userServiceDAO;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            bool isAuthorized = await _userServiceDAO.ValidateUserLoginAsync(request.Email, request.Password);

            if (isAuthorized == false)
            {
                throw new UnauthorizedAccessException();
            }

            // Generate JWT Token
            var token = _tokenService.GenerateToken(request.Email);

            // Return DTO
            return new LoginResponseDTO
            {
                Token = token,
                Email = request.Email,

            };
        }

    }
}