using Microsoft.EntityFrameworkCore;
using notification.API.Models;

namespace notification.API.Data;

// The class name must be correct here:
public class ApplicationDbContext : DbContext
{
    // The constructor signature must be correct here:
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<UserPreference> UserPreferences { get; set; }
}
