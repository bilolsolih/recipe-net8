using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Recipes.Models;

public enum Difficulty
{
  Easy,
  Medium,
  Hard,
}

public class Recipe
{
  public int Id { get; set; }

  public required int UserId { get; set; }
  public required User User { get; set; }

  public required int CategoryId { get; set; }
  public required Category Category { get; set; }
  
  public required string Title { get; set; }
  public required string Description { get; set; }
  public string? Photo { get; set; }
  public string? VideoRecipe { get; set; }
  public int TimeRequired { get; set; }
  public bool IsTrending { set; get; }
  public double Rating { get; set; }
  public required Difficulty Difficulty { get; set; }

  public ICollection<User> LikedUsers { get; set; } = new List<User>();
  public ICollection<Ingredient>? Ingredients { get; set; }
  public ICollection<Instruction>? Instructions { get; set; }
  public ICollection<Review> Reviews { get; set; } = new List<Review>();

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}