using AutoMapper;
using RecipeBackend.Core;
using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Notifications.DTOs;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Notifications.Repositories;

namespace RecipeBackend.Features.Notifications.Services;

public class NotificationIconService(
  NotificationIconRepository iconRepo,
  IWebHostEnvironment webEnv,
  IMapper mapper,
  IHttpContextAccessor httpContextAccessor) : ServiceBase("icons", webEnv, httpContextAccessor)
{
  public async Task<NotificationIcon> CreateNotificationIconAsync(NotificationIconCreateDto payload)
  {
    var alreadyExists = await iconRepo.ExistsByTitleAsync(payload.Title);
    AlreadyExistsException.ThrowIf(alreadyExists, payload.ToString());

    var fileName = await SaveUploadsFileAsync(payload.Icon);

    var newNotificationIcon = mapper.Map<NotificationIcon>(payload);

    newNotificationIcon.Icon = fileName;
    var notificationIcon = await iconRepo.AddAsync(newNotificationIcon);
    return notificationIcon;
  }

  public async Task<NotificationIconDetailDto> GetNotificationIconById(int id)
  {
    var notificationIcon = await iconRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notificationIcon, nameof(NotificationIcon));

    return mapper.Map<NotificationIconDetailDto>(notificationIcon);
  }

  public async Task<IEnumerable<NotificationIconListDto>> GetAllNotificationIcons()
  {
    var icons = await iconRepo.GetAllAsync();
    icons.ForEach(icon => icon.Icon = $"{BaseUrl}/{icon.Icon}");
    return mapper.Map<List<NotificationIconListDto>>(icons);
  }

  public async Task DeleteNotificationIconAsync(int id)
  {
    var notificationIcon = await iconRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(notificationIcon, nameof(NotificationIcon));

    DeleteUploadsFile(notificationIcon.Icon);

    await iconRepo.RemoveAsync(notificationIcon);
  }
}