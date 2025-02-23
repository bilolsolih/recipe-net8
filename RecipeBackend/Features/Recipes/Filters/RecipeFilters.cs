namespace RecipeBackend.Features.Recipes.Filters;

public class RecipeFilters
{
    public int? UserId { get; set; }
    public int? Category { get; set; }
    public bool? IsTrending { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? Order { get; set; }
    public bool? Descending { get; set; } = true;
}