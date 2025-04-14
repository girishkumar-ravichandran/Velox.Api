using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO.ResponseDTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Features.User.Handlers
{
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ForgetPasswordResponseDTO>
    {
        private readonly IUserServiceDAO _userServiceDAO;
        private readonly IPasswordService _passwordService;

        public ForgetPasswordCommandHandler(IUserServiceDAO userServiceDAO, IPasswordService passwordService)
        {
            _userServiceDAO = userServiceDAO;
            _passwordService = passwordService;
        }

        public async Task<ForgetPasswordResponseDTO> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var salt = _passwordService.GenerateSalt(request.Email);
            var passwordHash = _passwordService.HashPassword(request.NewPasswordHash, salt);
            var result = await _userServiceDAO.ForgetPasswordAsync(request.Email, request.NewPasswordHash);

            return new ForgetPasswordResponseDTO
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message
            };
        }
    }
}
