namespace RecipeBackend.Features.Recipes.DTOs;

public class ReviewCreateDto
{
    public required int RecipeId { get; set; }
    public required string Comment { get; set; }
    public required int Rating { get; set; }
    public required bool Recommend { get; set; }
    public IFormFile? Image { get; set; }
}

public class ReviewDetailDto
{
    public required int Id { get; set; }
    public required string Comment { get; set; }
    public required int Rating { get; set; }
    public string? Image { get; set; }
    public required UserInReviewsDto User { get; set; }
    public required DateTime Created { get; set; }
}

public class UserInReviewsDto
{
    public required int Id { get; set; }
    public string? ProfilePhoto { get; set; }
    public required string Username { get; set; }
}