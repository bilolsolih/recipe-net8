using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Repositories;

public class NotificationTemplateRepository(RecipeDbContext context)
{
  public async Task<NotificationTemplate> AddAsync(NotificationTemplate notificationTemplate)
  {
    context.NotificationTemplates.Add(notificationTemplate);
    await context.SaveChangesAsync();
    return notificationTemplate;
  }

  public async Task<NotificationTemplate?> GetByIdAsync(int id)
  {
    var template = await context.NotificationTemplates
      .Where(template => template.Id == id)
      .Include(template => template.NotificationIcon)
      .SingleOrDefaultAsync();
    return template;
  }

  public async Task<List<NotificationTemplate>> GetAllAsync()
  {
    return await context.NotificationTemplates
      .Include(template=>template.NotificationIcon)
      .ToListAsync();
  }

  public async Task<bool> ExistsByIdAsync(int id)
  {
    return await context.NotificationTemplates.AnyAsync(template => template.Id == id);
  }

  public async Task<bool> ExistsByTitleAsync(string title)
  {
    return await context.NotificationTemplates.AnyAsync(icon => icon.Title.ToLower() == title.ToLower());
  }

  public async Task RemoveAsync(NotificationTemplate notificationTemplate)
  {
    context.NotificationTemplates.Remove(notificationTemplate);
    await context.SaveChangesAsync();
  }
}