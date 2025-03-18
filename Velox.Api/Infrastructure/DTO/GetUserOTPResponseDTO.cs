namespace Velox.Api.Infrastructure.DTO
{
    public class GetUserOTPResponseDTO
    {
        public string OTP { get; set; }
        public string SMTP { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
