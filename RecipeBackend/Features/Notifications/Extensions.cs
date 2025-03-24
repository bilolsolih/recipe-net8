using RecipeBackend.Features.Notifications.Repositories;
using RecipeBackend.Features.Notifications.Services;

namespace RecipeBackend.Features.Notifications;

public static class Extensions
{
  public static void RegisterNotificationsFeature(this IServiceCollection services)
  {
    services.AddScoped<NotificationIconRepository>();
    services.AddScoped<NotificationIconService>();
    
    services.AddScoped<NotificationTemplateRepository>();
    services.AddScoped<NotificationTemplateService>();
  }
}