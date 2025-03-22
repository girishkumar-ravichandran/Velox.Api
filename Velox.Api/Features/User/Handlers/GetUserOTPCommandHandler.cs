using MediatR;
using Velox.Api.Features.User.Commands;
using Velox.Api.Infrastructure.DTO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services.Interfaces;


namespace Velox.Api.Features.User.Handlers
{
    public class GetUserOTPCommandHandler : IRequestHandler<GetUserOTPCommand, GetUserOTPResponseDTO>
    {
        private readonly IUserServiceDAO _userServiceDAO;
        private readonly IEmailService _emailService;

        public GetUserOTPCommandHandler(IUserServiceDAO userServiceDAO, IEmailService emailService)
        {
            _userServiceDAO = userServiceDAO;
            _emailService = emailService;
        }

        public async Task<GetUserOTPResponseDTO> Handle(GetUserOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _userServiceDAO.GetUserOTPAsync(request.Username);

            var EmailSucess = _emailService.SendEmail("OTP for Verification", $"Your Otp is {result.otp}", request.Username, "girishkumar.ravichandran@gmail.com", "vljz qltv yamy lmoj");

            return new GetUserOTPResponseDTO
            {
                OTP = result.otp,
                SMTP = result.smtp,
                IsSuccess = result.isSuccess,
                Message = result.message
            };
        }
    }
}
