namespace Recipe.Features.Recipes.Models;

public class Ingredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Title { get; set; }
    public string Amount { get; set; }

    public Recipe Recipe { get; set; }
}