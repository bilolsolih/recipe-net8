using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Features.Onboarding.DTOs;
using RecipeBackend.Features.Onboarding.Models;
using RecipeBackend.Features.Onboarding.Services;

namespace RecipeBackend.Features.Onboarding.Controllers;

[ApiController, Route("api/v1/onboarding")]
public class OnboardingController(OnboardingService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<OnboardingPage> Create(OnboardingPageCreateDto payload)
    {
        var newOnboardingPage = await service.CreateAsync(payload);
        return newOnboardingPage;
    }

    [HttpGet("list")]
    public async Task<IList<OnboardingPage>> List()
    {
        var onboardingPages = await service.ListAsync();
        return onboardingPages;
    }

    // [HttpPatch("update/{id:int}")]
    // public async Task<OnboardingPage> Update(int id, OnboardingPageUpdateDto payload)
    // {
    //     var updatedOnboardingPage = await service.UpdateOnboardingPageAsync(id, payload);
    //     return updatedOnboardingPage;
    // }
}