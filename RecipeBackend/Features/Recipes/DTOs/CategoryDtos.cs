namespace RecipeBackend.Features.Recipes.DTOs;

public class CategoryCreateDto
{
    public string Title { get; set; } = string.Empty;
    public IFormFile Photo { get; set; }
}

public class CategoryDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}

public class CategoryListDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}

public class CategoryUpdateDto
{
    public string? Title { get; set; }
    public IFormFile? Photo { get; set; }
}