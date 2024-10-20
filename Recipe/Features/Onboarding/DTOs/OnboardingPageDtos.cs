namespace Recipe.Features.Onboarding.DTOs;

public class OnboardingPageCreateDto
{
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Picture { get; set; }
    public int Order { get; set; }
}

public class OnboardingPageUpdateDto
{
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Picture { get; set; }
    public int? Order { get; set; }
}

public class OnboardingPageListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Picture { get; set; }
    public int Order { get; set; }
}