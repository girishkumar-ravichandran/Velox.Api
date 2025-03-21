﻿using Velox.Api.Infrastructure.DTO;
using MediatR;
using Velox.Api.Middleware.Enum;

namespace Velox.Api.Features.User.Commands
{
    public class RegisterUserCommand : IRequest<UserDTO>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public DateTime DOB { get; set; }

    }
}
