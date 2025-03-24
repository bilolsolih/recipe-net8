using AutoMapper;
using RecipeBackend.Features.Onboarding.DTOs;
using RecipeBackend.Features.Onboarding.Models;

namespace RecipeBackend.Features.Onboarding.Profiles;

public class OnboardingPageProfile : Profile
{
  public OnboardingPageProfile()
  {
    CreateMap<OnboardingPage, OnboardingPageListDto>();
    CreateMap<OnboardingPageCreateDto, OnboardingPage>();
    CreateMap<OnboardingPageUpdateDto, OnboardingPage>()
      .ForAllMembers(
        opts => opts.Condition(
          (src, dest, obj) =>
          {
            if (obj is string str)
            {
              return !string.IsNullOrEmpty(str);
            }

            return obj != null;
          }
        )
      );
  }
}