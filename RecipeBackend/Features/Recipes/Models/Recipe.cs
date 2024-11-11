using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Recipes.Models;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class Recipe
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public string VideoRecipe { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public int TimeRequired { get; set; }

    public bool IsPublished { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
    public ICollection<Instruction>? Instructions { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}