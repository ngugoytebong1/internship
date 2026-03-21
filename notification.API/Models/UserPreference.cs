namespace notification.API.Models;

public class UserPreference
{
    public int Id { get; set; }
    public int UserId { get; set; } // Links to the User Model

    // This is where we control the "Right Person" logic.
    public bool ReceiveSystemAlerts { get; set; } // High-priority (e.g., password change)
    public bool ReceiveMarketing { get; set; }    // Low-priority (opt-in/opt-out)
    public bool ReceiveOrderUpdates { get; set; } // Transactional (usually required)

    // For the "Right Time" logic (e.g., do not disturb hours)
    public bool IsDoNotDisturbActive { get; set; }
    // We can add a Start/EndTime property later if needed.

    public virtual User User { get; set; } // EF Core Navigation Property
}
