namespace RecipeBackend.Features.Customization.DTOs;

public class AllergicIngredientCreateDto
{
    public string Title { get; set; }
    public IFormFile Image { get; set; }
}

public class AllergicIngredientListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
}