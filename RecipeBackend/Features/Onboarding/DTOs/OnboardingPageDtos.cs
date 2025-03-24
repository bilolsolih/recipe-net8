namespace RecipeBackend.Features.Onboarding.DTOs;

public class OnboardingPageCreateDto
{
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required IFormFile Picture { get; set; }
    public int? Order { get; set; }
}

public class OnboardingPageUpdateDto
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public IFormFile? Picture { get; set; }
    public int? Order { get; set; }
}

public class OnboardingPageListDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string Picture { get; set; }
    public int Order { get; set; }
}