using RecipeBackend.Features.Onboarding.Repositories;
using RecipeBackend.Features.Onboarding.Services;

namespace RecipeBackend.Features.Onboarding;

public static class Extensions
{
    public static void RegisterOnboardingFeature(this IServiceCollection services)
    {
        services.AddScoped<OnboardingRepository>();
        services.AddScoped<OnboardingService>();
    }
}