using Recipe.Features.Onboarding.Repositories;
using Recipe.Features.Onboarding.Services;
using Recipe.Features.Onboarding.Data;

namespace Recipe.Features.Onboarding;

public static class Extensions
{
    public static void RegisterOnboardingFeature(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddNpgsql<OnboardingContext>(configuration.GetConnectionString("DefaultConnection"));

        services.AddScoped<OnboardingRepository>();
        services.AddScoped<OnboardingService>();
    }
}