namespace Velox.Api.Middleware.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string subject, string body, string toEmail, string fromEmail, string password);
    };
}
