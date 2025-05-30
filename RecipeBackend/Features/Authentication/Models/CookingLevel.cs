namespace RecipeBackend.Features.Authentication.Models;

public class CookingLevel
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }

  public ICollection<User> Users { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}