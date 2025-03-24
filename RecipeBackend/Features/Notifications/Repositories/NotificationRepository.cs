using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Repositories;

public class NotificationRepository(RecipeDbContext context)
{
  public async Task<Notification> AddAsync(Notification notification)
  {
    context.Notifications.Add(notification);
    await context.SaveChangesAsync();
    return notification;
  }

  public async Task<Notification?> GetByIdAsync(int id)
  {
    var notification = await context.Notifications
      .Where(notification => notification.Id == id)
      .Include(notification => notification.NotificationTemplate)
      .SingleOrDefaultAsync();
    return notification;
  }

  public async Task<List<Notification>> GetAllAsync()
  {
    return await context.Notifications
      .Include(notification => notification.NotificationTemplate)
      .OrderByDescending(notification => notification.ScheduledDate)
      .Where(notification => notification.ScheduledDate <= DateTime.UtcNow)
      .ToListAsync();
  }

  public async Task<bool> ExistsByIdAsync(int id)
  {
    return await context.Notifications.AnyAsync(notification => notification.Id == id);
  }

  public async Task RemoveAsync(Notification notification)
  {
    context.Notifications.Remove(notification);
    await context.SaveChangesAsync();
  }
}