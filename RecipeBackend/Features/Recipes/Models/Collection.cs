using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Recipes.Models;

public class Collection
{
    public int Id { get; set; }
    
    public required int UserId { get; set; }
    public required User User { get; set; }
    
    public required string Title { get; set; }
    public required string Image { get; set; }
    
    public ICollection<Recipe> Recipes { get; set; } = [];
}