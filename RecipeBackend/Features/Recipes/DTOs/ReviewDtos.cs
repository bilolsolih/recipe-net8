namespace RecipeBackend.Features.Recipes.DTOs;

public class ReviewCreateDto
{
    public required int RecipeId { get; set; }
    public required string Comment { get; set; }
    public required int Rating { get; set; }
    public required bool Recommend { get; set; }
    public IFormFile? Image { get; set; }
}