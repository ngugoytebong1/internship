using notification.API.Models;
using System.Threading.Tasks;

namespace notification.API.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync(); // Commit the change to the database
        }
    }
}
