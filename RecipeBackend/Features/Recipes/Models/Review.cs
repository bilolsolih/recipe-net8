using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Recipes.Models;

public class Review
{
    public int Id { get; set; }
    public User Author { get; set; }
    public Recipe Recipe { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}