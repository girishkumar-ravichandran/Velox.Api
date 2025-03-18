using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Features.User.Handlers
{
    public class ValidateUserOTPCommandHandler : IRequestHandler<ValidateUserOTPCommand, ValidateUserOTPResponseDTO>
    {
        private readonly IUserServiceDAO _userServiceDAO;
        private readonly ITokenService _tokenService;

        public ValidateUserOTPCommandHandler(IUserServiceDAO userServiceDAO, ITokenService tokenService)
        {
            _userServiceDAO = userServiceDAO;
        }

        public async Task<ValidateUserOTPResponseDTO> Handle(ValidateUserOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _userServiceDAO.ValidateUserOTPAsync(request.Username, request.OTP);

            return new ValidateUserOTPResponseDTO
            {
                IsSuccess = result.isSuccess,
                Message = result.message
            };
        }
    }
}
