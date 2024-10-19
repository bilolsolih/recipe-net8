namespace Recipe.Features.Recipes.Models;

public class Recipe
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string? VideoRecipe { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string TimeRequired { get; set; }
    public string Photo { get; set; }

    public bool IsPublished { get; set; }

    public Category Category { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
}