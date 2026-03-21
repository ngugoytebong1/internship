using notification.API.Models;
using System.Threading.Tasks;

namespace notification.API.Data
{
    public interface IUserRepository
    {
        // Method to get a user by their ID
        Task<User> GetUserByIdAsync(int userId);

        // Method to get a user's preferences by their ID
        Task<UserPreference> GetUserPreferencesAsync(int userId);

        // Method to get a user by their email address (e.g., if a system triggers an email)
        Task<User> GetUserByEmailAsync(string email);
    }
}
