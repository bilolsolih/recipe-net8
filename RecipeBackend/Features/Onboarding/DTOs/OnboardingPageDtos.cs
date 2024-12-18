namespace RecipeBackend.Features.Onboarding.DTOs;

public class OnboardingPageCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public IFormFile Picture { get; set; }
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
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public int Order { get; set; }
}