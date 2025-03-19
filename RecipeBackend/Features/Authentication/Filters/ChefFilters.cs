namespace RecipeBackend.Features.Authentication.Filters;

public enum OrderBy {Views, Likes, Date}
public class ChefFilters
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public OrderBy? Order { get; set; }
    public bool Descending { get; set; } = true;
}