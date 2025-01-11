namespace RecipeBackend.Features.Recipes.DTOs;

public class CategoryCreateDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile Image { get; set; }
    public bool Main { get; set; } = false;
}

public class CategoryDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public bool Main { get; set; }
}

public class CategoryListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public bool Main { get; set; }
}

public class CategoryUpdateDto
{
    public string? Title { get; set; }
    public IFormFile? Image { get; set; }
    public bool? Main { get; set; }
}