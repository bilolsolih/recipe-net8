namespace RecipeBackend.Features.Customization.Models;

public class Cuisine
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Image { get; set; }
}