namespace RecipeBackend.Features.Recipes.DTOs;

public class RecipeCreateDto
{
    public int CategoryId { get; set; }
    public IFormFile VideoRecipe { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TimeRequired { get; set; }

    // public IList<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    // public IList<InstructionDto> Instructions { get; set; } = new List<InstructionDto>();
}



public class InstructionDto
{
    public string Text { get; set; } = string.Empty;
    public int Order { get; set; }
}