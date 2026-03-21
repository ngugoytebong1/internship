using Microsoft.EntityFrameworkCore;
using notification.API.Models;
using System.Threading.Tasks;

namespace notification.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Receives the DbContext via Dependency Injection (DI)
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            // Simple EF Core lookup by primary key
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserPreference> GetUserPreferencesAsync(int userId)
        {
            // EF Core lookup for preferences, filtering by UserId
            return await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            // EF Core lookup for user by email address
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmailAddress == email);
        }
    }
}
