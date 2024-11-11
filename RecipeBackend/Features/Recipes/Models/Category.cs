namespace RecipeBackend.Features.Recipes.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;


    public ICollection<Recipe>? Recipes { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}