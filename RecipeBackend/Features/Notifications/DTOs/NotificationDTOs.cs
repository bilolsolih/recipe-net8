namespace RecipeBackend.Features.Notifications.DTOs;

public record NotificationDetailDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  
  public required DateTime SentDate { get; set; }
  public required DateTime Created { get; set; }
  public required DateTime Updated { get; set; }
}

public record NotificationListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  public required DateTime Date { get; set; }
}

public record NotificationCreateDto
{
  public required int NotificationTemplateId { get; set; }
  public required bool SendNow { get; set; } = false;
  public required DateTime ScheduledDate { get; set; }
}

public record NotificationUpdateDto
{
  public string? Title { get; set; }
  public string? Subtitle { get; set; }
  public DateTime? ScheduledDate { get; set; }
}