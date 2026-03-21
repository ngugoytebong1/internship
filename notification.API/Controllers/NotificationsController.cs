using Microsoft.AspNetCore.Mvc;
using notification.API.Services;
using notification.API.DTOs;
using System.Threading.Tasks;

// Controller Route: Defines the base path as 'api/notifications'
[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    // Dependency Injection: The Controller requests the Orchestrator Service
    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // Endpoint: POST api/notifications/send
    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationTriggerDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 1. Delegate the complex work to the Service Layer
        bool success = await _notificationService.ProcessNotificationAsync(
            request.NotificationType,
            request.UserId);

        // 2. Format the HTTP Response
        if (success)
        {
            return Ok(new { message = "Notification process initiated successfully." });
        }

        // This includes cases where the service decided NOT to send (e.g., user preference skip)
        return StatusCode(500, new { message = "Notification processing failed or was blocked by preferences." });
    }
}
