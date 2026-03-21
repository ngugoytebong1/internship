namespace notification.API.Models;

public class Notification
{
    public int Id { get; set; }
    public int RecipientId { get; set; } // Links to the User Model
    public string NotificationType { get; set; } = null!; // e.g., "ORDER_CONFIRMATION", "LOW_STOCK_ALERT"
    public string Subject { get; set; } = null!;
    public string BodyHtml { get; set; } = null!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = null!; // e.g., "PENDING", "SENT", "FAILED"
}
