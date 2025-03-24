namespace RecipeBackend.Features.Notifications.DTOs;

public record NotificationIconDetailDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }
  
  public required DateTime Created { get; set; }
  public required DateTime Updated { get; set; }
}

public record NotificationIconListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }
}

public record NotificationIconCreateDto
{
  public required string Title { get; set; }
  public required IFormFile Icon { get; set; }
}

public record NotificationIconUpdateDto
{
  public string? Title { get; set; }
  public IFormFile? Icon { get; set; }
}