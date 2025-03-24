using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Services;

namespace RecipeBackend.Features.Notifications.Controllers;

[ApiController, Route("api/v1/notification-icons")]
public class NotificationIconController(NotificationIconService service) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<NotificationIcon>> CreateNotificationIcon([FromForm] NotificationIconCreateDto payload)
  {
    var notificationIcon = await service.CreateNotificationIconAsync(payload);
    return CreatedAtAction(nameof(GetNotificationIcon), new { id = notificationIcon.Id }, notificationIcon);
  }

  [HttpGet("retrieve/{id:int}")]
  public async Task<ActionResult<NotificationIconDetailDto>> GetNotificationIcon(int id)
  {
    var notificationIcon = await service.GetNotificationIconById(id);
    return Ok(notificationIcon);
  }
  
  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<NotificationIconListDto>>> GetAllNotificationIcons()
  {
    var templates = await service.GetAllNotificationIcons();
    return Ok(templates);
  }
  
  
  [HttpDelete("delete/{id:int}")]
  public async Task<ActionResult> DeleteNotificationIcon(int id)
  {
    await service.DeleteNotificationIconAsync(id);
    return NoContent();
  }
}