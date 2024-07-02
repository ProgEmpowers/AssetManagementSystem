using System.Net.Mail;
using System.Net;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AssetManagementSystem.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromPassword;

        public EmailService(IConfiguration configuration)
        {
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            _fromEmail = configuration["EmailSettings:FromEmail"];
            _fromPassword = configuration["EmailSettings:FromPassword"];
        }
        public async Task SendEmailAsync(string resiverEmail, string subject, string message)
        {
            try
            {
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_fromEmail, _fromPassword)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(resiverEmail);

                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException e)
            {
                // Log or handle the exception as needed.
                Console.WriteLine($"SMTP Exception: {e.Message}");
                throw;  // Rethrow or handle accordingly.
            }
            catch (Exception e)
            {
                // Log or handle the exception as needed.
                Console.WriteLine($"General Exception: {e.Message}");
                throw;  //Rethrow or handle accordinglty.
            }

        }
    }
}
