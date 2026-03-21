using System.Threading.Tasks;

namespace notification.API.Services
{
    public interface IEmailSenderService
    {
        // This method handles the actual API call to the email provider
        Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent);
    }
}
