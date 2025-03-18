namespace RecipeBackend.Features.Authentication.Filters;

public class ChefFilters
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? Order { get; set; }
    public bool? Descending { get; set; }
}