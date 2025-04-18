﻿using MediatR;
using Velox.Api.Infrastructure.DTO.ResponseDTO;

namespace Velox.Api.Features.User.Commands
{
    public class ValidateUserOTPCommand : IRequest<ValidateUserOTPResponseDTO>
    {
        public string Username { get; set; }
        public string OTP { get; set; }
    }
}
