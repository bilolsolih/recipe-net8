namespace RecipeBackend.Features.Notifications.Models;

public class NotificationIcon
{
  public int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}