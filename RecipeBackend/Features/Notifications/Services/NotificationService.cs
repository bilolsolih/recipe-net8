using AutoMapper;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Repositories;

namespace RecipeBackend.Features.Notifications.Services;

public class NotificationService(
  NotificationRepository notificationRepo,
  NotificationTemplateRepository templateRepo,
  IWebHostEnvironment webEnv,
  IMapper mapper)
{
  public async Task<Notification> CreateNotificationAsync(NotificationCreateDto payload)
  {
    var templateExists = await templateRepo.ExistsByIdAsync(payload.NotificationTemplateId);
    DoesNotExistException.ThrowIfNot(templateExists, nameof(NotificationTemplate));

    var newNotification = mapper.Map<Notification>(payload);

    return await notificationRepo.AddAsync(newNotification);
  }

  public async Task<NotificationDetailDto> GetNotificationById(int id)
  {
    var notification = await notificationRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notification, nameof(Notification));

    return mapper.Map<NotificationDetailDto>(notification);
  }

  public async Task<IEnumerable<NotificationListDto>> GetAllNotifications()
  {
    var notifications = await notificationRepo.GetAllAsync();
    return mapper.Map<List<NotificationListDto>>(notifications);
  }


  public async Task DeleteNotificationAsync(int id)
  {
    var notification = await notificationRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notification, nameof(Notification));

    await notificationRepo.RemoveAsync(notification);
  }
}