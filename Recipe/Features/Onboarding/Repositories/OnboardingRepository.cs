using Recipe.Features.Onboarding.Models;
using Recipe.Features.Onboarding.Data;

namespace Recipe.Features.Onboarding.Repositories;

public class OnboardingRepository(OnboardingContext context)
{
    public OnboardingPage Create(OnboardingPage onboardingPage)
    {
        context.OnboardingPages.Add(onboardingPage);
        context.SaveChanges();
        return onboardingPage;
    }

    public IList<OnboardingPage> List()
    {
        var onboardingPages = context.OnboardingPages.ToList();
        return onboardingPages;
    }
}