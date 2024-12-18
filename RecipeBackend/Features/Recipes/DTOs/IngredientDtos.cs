using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.DTOs;

public class IngredientCreateDto
{
    public int RecipeId { get; set; }
    public string Amount { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }

    public Recipe Recipe { get; set; }
}