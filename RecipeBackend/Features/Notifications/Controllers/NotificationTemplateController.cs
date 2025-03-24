using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Services;

namespace RecipeBackend.Features.Notifications.Controllers;

[ApiController, Route("api/v1/notification-templates")]
public class NotificationTemplateController(NotificationTemplateService service) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<NotificationTemplate>> CreateNotificationTemplate([FromForm] NotificationTemplateCreateDto payload)
  {
    var notificationTemplate = await service.CreateNotificationTemplateAsync(payload);
    return CreatedAtAction(nameof(GetNotificationTemplate), new { id = notificationTemplate.Id }, notificationTemplate);
  }

  [HttpGet("retrieve/{id:int}")]
  public async Task<ActionResult<NotificationTemplateDetailDto>> GetNotificationTemplate(int id)
  {
    var notificationTemplate = await service.GetNotificationTemplateById(id);
    return Ok(notificationTemplate);
  }

  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<NotificationTemplateListDto>>> GetAllNotificationTemplates()
  {
    var templates = await service.GetAllNotificationTemplates();
    return Ok(templates);
  }

  [HttpDelete("delete/{id:int}")]
  public async Task<ActionResult> DeleteNotificationTemplate(int id)
  {
    await service.DeleteNotificationTemplateAsync(id);
    return NoContent();
  }
}