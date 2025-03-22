using System.Net;
using System.Net.Mail;
using Velox.Api.Middleware.Services.Interfaces;

namespace Velox.Api.Middleware.Services
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string subject, string body, string toEmail, string fromEmail, string password)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false, 
                };

                mailMessage.To.Add(toEmail);

                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
