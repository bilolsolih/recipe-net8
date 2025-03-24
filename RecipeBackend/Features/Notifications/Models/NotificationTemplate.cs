namespace RecipeBackend.Features.Notifications.Models;

public class NotificationTemplate
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  
  public required int NotificationIconId { get; set; }
  public required NotificationIcon NotificationIcon { get; set; }

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}