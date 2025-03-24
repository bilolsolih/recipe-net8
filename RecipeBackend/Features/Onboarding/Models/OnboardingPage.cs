namespace RecipeBackend.Features.Onboarding.Models;

public class OnboardingPage
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Subtitle { get; set; }
    public required string Image { get; set; }
    public int Order { get; set; }
}