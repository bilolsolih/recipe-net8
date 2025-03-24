using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Services;

namespace RecipeBackend.Features.Notifications.Controllers;

[ApiController, Route("api/v1/notifications")]
public class NotificationController(NotificationService service) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<Notification>> CreateNotification(NotificationCreateDto payload)
  {
    var notificationTemplate = await service.CreateNotificationAsync(payload);
    return CreatedAtAction(nameof(GetNotification), new { id = notificationTemplate.Id }, notificationTemplate);
  }

  [HttpGet("retrieve/{id:int}")]
  public async Task<ActionResult<NotificationDetailDto>> GetNotification(int id)
  {
    var notificationTemplate = await service.GetNotificationById(id);
    return Ok(notificationTemplate);
  }

  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<NotificationListDto>>> GetAllNotifications()
  {
    var notifications = await service.GetAllNotifications();
    return Ok(notifications);
  }

  [HttpDelete("delete/{id:int}")]
  public async Task<ActionResult> DeleteNotification(int id)
  {
    await service.DeleteNotificationAsync(id);
    return NoContent();
  }
}