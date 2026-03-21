// DTOs/NotificationTriggerDto.cs

using System.ComponentModel.DataAnnotations;

namespace notification.API.DTOs
{
    public class NotificationTriggerDto
    {
        [Required]
        public int UserId { get; set; } // Identifies the recipient

        [Required]
        public string NotificationType { get; set; } // e.g., "ORDER_CONFIRMATION", "LOW_STOCK_ALERT"

        // You can add data fields here if the notification needs dynamic content,
        // e.g., public string EventData { get; set; }
    }
}
