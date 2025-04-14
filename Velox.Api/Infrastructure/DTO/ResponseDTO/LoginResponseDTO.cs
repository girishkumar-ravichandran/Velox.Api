namespace Velox.Api.Infrastructure.DTO.ResponseDTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public bool IsLoginSuccess { get; set; }
        public bool IsUserLocked { get; set; }
        public bool IsUserValidated { get; set; }
        public bool IsPendingRegistration { get; set; }
    }
}
