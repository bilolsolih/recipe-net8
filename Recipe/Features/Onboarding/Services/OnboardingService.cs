using AutoMapper;
using Recipe.Features.Onboarding.Models;
using Recipe.Features.Onboarding.Repositories;
using Recipe.Features.Onboarding.DTOs;

namespace Recipe.Features.Onboarding.Services;

public class OnboardingService(OnboardingRepository repository, IMapper mapper)
{
    public OnboardingPage Create(OnboardingPageCreateDto payload)
    {
        var newOnboardingPage = mapper.Map<OnboardingPage>(payload);
        return repository.Create(newOnboardingPage);
    }

    public IList<OnboardingPageListDto> List()
    {
        var onboardingPages = repository.List();
        var onboardingPagesList = mapper.Map<IList<OnboardingPageListDto>>(onboardingPages);
        return onboardingPagesList;
    }
}