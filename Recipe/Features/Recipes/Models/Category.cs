namespace Recipe.Features.Recipes.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Photo { get; set; }

    public ICollection<Recipe>? Recipes { get; set; }
}