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
  public string Username { get; set; } = string.Empty;
  public string? ProfilePhoto { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string? Presentation { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PhoneNumber { get; set; } = string.Empty;
  public DateOnly BirthDate { get; set; }
  public Gender? Gender { get; set; }
  public string Password { get; set; } = string.Empty;

  public ICollection<Recipe> Recipes { get; set; } = [];
  public ICollection<Recipe> LikedRecipes { get; set; } = [];
  public ICollection<Review> Reviews { get; set; } = [];
  public ICollection<User> Followers { get; set; } = [];
  public ICollection<User> Followings { get; set; } = [];

  public DateTime Created { get; set; }
  public DateTime Updated { get; set; }
}