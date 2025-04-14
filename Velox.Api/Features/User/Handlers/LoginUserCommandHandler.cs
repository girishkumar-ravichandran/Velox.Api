using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO.ResponseDTO;
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
            var result = await _userServiceDAO.ValidateUserLoginAsync(request.Email, request.Password);

            if (!result.IsLoginSuccess)
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
                IsLoginSuccess = result.IsLoginSuccess,
                IsUserLocked = result.IsUserLocked,
                IsUserValidated = result.IsUserValidated,
                IsPendingRegistration = result.IsPendingRegistration
            };
        }

    }
}