namespace RecipeBackend.Features.Recipes.DTOs;

public class RecipeCreateDto
{
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int TimeRequired { get; set; }

    public IList<InstructionDto> Instructions { get; set; } = new List<InstructionDto>();
    public IList<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
}

public class RecipeListDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }
    public int TimeRequired { get; set; }
    public double Rating { get; set; }
}

public class RecipeListCommunityDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Photo { get; set; }
    public required int TimeRequired { get; set; }
    public double Rating { get; set; }
    public int ReviewsCount { get; set; }
    public DateTime Created { get; set; }

    public required UserForRecipeDetailDto User { get; set; }
}

public class RecipeDetailDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }
    public string? VideoRecipe { get; set; }
    public int TimeRequired { get; set; }
    public double Rating { get; set; }
    public int ReviewsCount { get; set; }
    public UserForRecipeDetailDto User { get; set; }

    public IList<InstructionDto> Instructions { get; init; } = new List<InstructionDto>();
    public IList<IngredientDto> Ingredients { get; init; } = new List<IngredientDto>();
}

public class RecipeDetailReviewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Photo { get; set; }
    public double Rating { get; set; }
    public int ReviewsCount { get; set; }
    public UserForRecipeDetailDto User { get; set; }
}

public class RecipeUpdateDto
{
    public int? CategoryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Photo { get; set; }
    public IFormFile? VideoRecipe { get; set; }
    public int? TimeRequired { get; set; }
}

public class UserForRecipeDetailDto
{
    public int Id { get; set; }
    public string? ProfilePhoto { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class InstructionDto
{
    public string Text { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class IngredientDto
{
    public string Amount { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
}