using System.Threading.Tasks;

namespace notification.API.Services
{
    public interface INotificationService
    {
        // This is the main method called to start a notification process.
        // It takes a basic event type and the user identifier.
        Task<bool> ProcessNotificationAsync(string notificationType, int userId);

        // Optional: A method to record the notification attempt in the database.
        Task RecordNotificationAttempt(string notificatioType, int userId, string status, string subject, string body);
    }
}
