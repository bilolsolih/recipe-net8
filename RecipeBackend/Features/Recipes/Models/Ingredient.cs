using System.Text.Json.Serialization;

namespace RecipeBackend.Features.Recipes.Models;

public class Ingredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Amount { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    [JsonIgnore]
    public Recipe Recipe { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}