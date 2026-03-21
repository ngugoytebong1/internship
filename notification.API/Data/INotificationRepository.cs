using notification.API.Models;
using System.Threading.Tasks;

namespace notification.API.Data
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
        // You can add methods like GetFailedNotificationsAsync later
    }
}
