using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Repositories;

public class NotificationIconRepository(RecipeDbContext context)
{
  public async Task<NotificationIcon> AddAsync(NotificationIcon notificationIcon)
  {
    context.NotificationIcons.Add(notificationIcon);
    await context.SaveChangesAsync();
    return notificationIcon;
  }

  public async Task<NotificationIcon?> GetByIdAsync(int id)
  {
    return await context.NotificationIcons.FindAsync(id);
  }

  public async Task<List<NotificationIcon>> GetAllAsync()
  {
    return await context.NotificationIcons.ToListAsync();
  }

  public async Task<bool> ExistsByIdAsync(int id)
  {
    return await context.NotificationIcons.AnyAsync(icon => icon.Id == id);
  }

  public async Task<bool> ExistsByTitleAsync(string title)
  {
    return await context.NotificationIcons.AnyAsync(icon => icon.Title.ToLower() == title.ToLower());
  }

  public async Task RemoveAsync(NotificationIcon notificationIcon)
  {
    context.NotificationIcons.Remove(notificationIcon);
    await context.SaveChangesAsync();
  }
}