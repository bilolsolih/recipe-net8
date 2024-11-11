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
    public string Username { get; set; }
    public string? ProfilePhoto { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public Gender? Gender { get; set; }
    public string Password { get; set; }

    public ICollection<Recipe> Recipes { get; set; }
    public ICollection<Review> Reviews { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}