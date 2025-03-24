using AutoMapper;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Repositories;

namespace RecipeBackend.Features.Notifications.Services;

public class NotificationTemplateService(
  NotificationTemplateRepository templateRepo,
  NotificationIconRepository iconRepo,
  IWebHostEnvironment webEnv,
  IMapper mapper)
{
  public async Task<NotificationTemplate> CreateNotificationTemplateAsync(NotificationTemplateCreateDto payload)
  {
    var alreadyExists = await templateRepo.ExistsByTitleAsync(payload.Title);
    AlreadyExistsException.ThrowIf(alreadyExists, payload.ToString());

    var iconExists = await iconRepo.ExistsByIdAsync(payload.NotificationIconId);
    DoesNotExistException.ThrowIfNot(iconExists, nameof(NotificationIcon));

    var newNotificationTemplate = mapper.Map<NotificationTemplate>(payload);

    var notificationTemplate = await templateRepo.AddAsync(newNotificationTemplate);
    return notificationTemplate;
  }

  public async Task<NotificationTemplateDetailDto> GetNotificationTemplateById(int id)
  {
    var notificationTemplate = await templateRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notificationTemplate, nameof(NotificationTemplate));

    return mapper.Map<NotificationTemplateDetailDto>(notificationTemplate);
  }

  public async Task<IEnumerable<NotificationTemplateListDto>> GetAllNotificationTemplates()
  {
    var templates = await templateRepo.GetAllAsync();
    return mapper.Map<List<NotificationTemplateListDto>>(templates);
  }
  

  public async Task DeleteNotificationTemplateAsync(int id)
  {
    var notificationTemplate = await templateRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notificationTemplate, nameof(NotificationTemplate));

    await templateRepo.RemoveAsync(notificationTemplate);
  }
}