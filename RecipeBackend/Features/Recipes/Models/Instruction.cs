using System.Text.Json.Serialization;

namespace RecipeBackend.Features.Recipes.Models;

public class Instruction
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public string Text { get; set; }
    public int Order { get; set; }

    [JsonIgnore]
    public Recipe Recipe { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}