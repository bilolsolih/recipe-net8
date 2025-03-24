namespace RecipeBackend.Features.Notifications.DTOs;

public record NotificationTemplateDetailDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  public required string Icon { get; set; }

  public required DateTime Created { get; set; }
  public required DateTime Updated { get; set; }
}

public record NotificationTemplateListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  public required string Icon { get; set; }
}

public record NotificationTemplateCreateDto
{
  public required string Title { get; set; }
  public required string Subtitle { get; set; }
  public required int NotificationIconId { get; set; }
}

public record NotificationTemplateUpdateDto
{
  public string? Title { get; set; }
  public string? Subtitle { get; set; }
  public int? Icon { get; set; }
}