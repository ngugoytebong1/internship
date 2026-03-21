using System.Threading.Tasks;
using Microsoft.Extensions.Logging; // Used for logging status

namespace notification.API.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> _logger;

        // In a real app, this would be injected with API keys/client objects
        public EmailSenderService(ILogger<EmailSenderService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            // --- Placeholder Logic ---

            // In a real application, you would connect to a service like SendGrid here.
            // Example: var client = new SendGridClient(apiKey);
            // Example: await client.SendEmailAsync(msg);

            // Simulate network delay and success
            await Task.Delay(100);

            _logger.LogInformation($"SUCCESS: Email sent to {toEmail} with subject: {subject}");

            // Return true to simulate a successful send
            return true;
        }
    }
}
