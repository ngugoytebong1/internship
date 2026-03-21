using Microsoft.Extensions.Logging;
using notification.API.Data;
using notification.API.Models;
using System.Threading.Tasks;

namespace notification.API.Services
{
    public class NotificationService : INotificationService
    {
        // Inside NotificationService.cs

        // Helper method to simulate checking the time constraints
        private bool IsDuringDoNotDisturb(UserPreference preferences)
        {
            // If the preference to use DND is false, we ignore time checks.
            if (!preferences.IsDoNotDisturbActive)
            {
                return false;
            }

            // --- Original Logic for Time Check (10 PM to 8 AM) ---
            var hour = DateTime.Now.Hour;

            // Check if the current hour is between 10 PM (22) and 8 AM (8)
            if (hour >= 22 || hour < 8)
            {
                return true; // It's DND time!
            }

            return false;
        }
        private readonly IUserRepository _userRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILogger<NotificationService> _logger;
        private readonly INotificationRepository _notificationRepository;
        // Dependency Injection: Injecting the required services (Repository and Sender)
        // Services/NotificationService.cs

        public NotificationService(
            IUserRepository userRepository,
            IEmailSenderService emailSenderService,
            INotificationRepository notificationRepository, // 👈 NEW PARAMETER
            ILogger<NotificationService> logger)
        {
            _userRepository = userRepository;
            _emailSenderService = emailSenderService;
            _notificationRepository = notificationRepository; // 👈 NEW ASSIGNMENT
            _logger = logger;
        }
        public async Task<bool> ProcessNotificationAsync(string notificationType, int userId)
        {
            _logger.LogInformation($"Processing notification type: {notificationType} for User ID: {userId}");

            // 1. Get User and Preferences (The "Right Person" check)
            var user = await _userRepository.GetUserByIdAsync(userId);
            var preferences = await _userRepository.GetUserPreferencesAsync(userId);

            if (user == null || preferences == null)
            {
                _logger.LogWarning($"User or preferences not found for ID: {userId}. Aborting.");
                return false;
            }

            // Define subject and body early so they can be logged if the send is skipped
            string subject = $"Your {notificationType} Update";
            string body = $"Hello {user.FullName}, this is your update for {notificationType}.";


            // --- START: CORE LOGIC (Preference and Time Rules) ---

            // 1. Check Do Not Disturb (The "Right Time" check)
            if (IsDuringDoNotDisturb(preferences))
            {
                await RecordNotificationAttempt(notificationType, userId, "SKIPPED_DND", subject, "Notification skipped due to user's Do Not Disturb hours.");
                return true;
            }


            // 2. Check Notification Type against Preferences (The "Right Message" check)
            bool shouldSend = true;
            string skipReason = string.Empty;

            switch (notificationType)
            {
                case "MARKETING":
                    if (preferences.ReceiveMarketing == false)
                    {
                        shouldSend = false;
                        skipReason = "User opted out of marketing.";
                    }
                    break;
                case "ORDER_CONFIRMATION":
                    if (preferences.ReceiveOrderUpdates == false)
                    {
                        shouldSend = false;
                        skipReason = "User opted out of order updates.";
                    }
                    break;
                case "SYSTEM_ALERT":
                    if (preferences.ReceiveSystemAlerts == false)
                    {
                        shouldSend = false;
                        skipReason = "User opted out of system alerts.";
                    }
                    break;
                default:
                    // If the type is unknown, we generally assume sending is allowed.
                    break;
            }

            if (!shouldSend)
            {
                await RecordNotificationAttempt(notificationType, userId, "SKIPPED_PREFERENCE", subject, skipReason);
                return true; // Successfully decided NOT to send
            }

            // --- END: CORE LOGIC ---


            // 3. Send Email (The "Right Channel" - Email)
            bool success = await _emailSenderService.SendEmailAsync(user.EmailAddress, subject, body);

            // 4. Record Result
            string status = success ? "SENT" : "FAILED";
            await RecordNotificationAttempt(notificationType, userId, status, subject, body);

            return success;
        }

        // Services/NotificationService.cs

        public async Task RecordNotificationAttempt(string notificationType, int userId, string status, string subject, string body)
        {
            // 1. Create the Notification model object
            var notification = new Models.Notification // Use Models.Notification to avoid ambiguity
            {
                RecipientId = userId,
                NotificationType = notificationType, // Use the type from the original trigger
                Subject = subject,
                Status = status,
                BodyHtml = body,                     // Save the actual body content
                SentAt = DateTime.UtcNow
            };

            // 2. Use the new repository to save the object
            await _notificationRepository.AddNotificationAsync(notification);

            // Log the action (optional, but good practice)
            _logger.LogInformation($"Notification recorded for User {userId} with status: {status}");
        }
    }
}
