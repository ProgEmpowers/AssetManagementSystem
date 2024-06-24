using AuthService.Models.Helpter;

namespace AuthService.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
