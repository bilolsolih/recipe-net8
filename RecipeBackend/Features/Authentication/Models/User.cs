using RecipeBackend.Features.Customization.Models;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Authentication.Models;

public enum Gender
{
  Male,
  Female
}

public class User
{
  public int Id { get; set; }
  public required string Username { get; set; }
  public string? ProfilePhoto { get; set; }
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public string? Presentation { get; set; }
  public required string Email { get; set; }
  public required string PhoneNumber { get; set; }
  public DateOnly BirthDate { get; set; }
  public Gender? Gender { get; set; }
  public string Password { get; set; }

  public ICollection<Recipe> Recipes { get; set; } = [];
  public ICollection<Recipe> LikedRecipes { get; set; } = [];
  public ICollection<Review> Reviews { get; set; } = [];
  public ICollection<User> Followers { get; set; } = [];
  public ICollection<User> Followings { get; set; } = [];
  public ICollection<AllergicIngredient> AllergicIngredients { get; set; } = [];
  public ICollection<Cuisine> Cuisines { get; set; } = [];

  public int? CookingLevelId { get; set; }
  public CookingLevel? CookingLevel { get; set; }

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}