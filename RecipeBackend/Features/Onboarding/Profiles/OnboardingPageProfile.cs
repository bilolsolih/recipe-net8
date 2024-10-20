using AutoMapper;
using RecipeBackend.Features.Onboarding.DTOs;
using RecipeBackend.Features.Onboarding.Models;

namespace RecipeBackend.Features.Onboarding.Profiles;

public class OnboardingPageProfile : Profile
{
    public OnboardingPageProfile()
    {
        CreateMap<OnboardingPage, OnboardingPageCreateDto>();
        CreateMap<OnboardingPage, OnboardingPageListDto>();
        CreateMap<OnboardingPage, OnboardingPageUpdateDto>();

        CreateMap<OnboardingPageCreateDto, OnboardingPage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<OnboardingPageListDto, OnboardingPage>();
        CreateMap<OnboardingPageUpdateDto, OnboardingPage>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}