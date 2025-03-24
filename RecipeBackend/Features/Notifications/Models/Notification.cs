namespace RecipeBackend.Features.Notifications.Models;

public class Notification
{
  public int Id { get; set; }
  
  public required int NotificationTemplateId { get; set; }
  public required NotificationTemplate NotificationTemplate { get; set; }
  
  public required DateTime ScheduledDate { get; set; }
  public required bool SendNow { get; set; }
  
  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}