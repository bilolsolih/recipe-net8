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
        return await service.CreateAsync(payload);
    }

    [HttpGet("list")]
    public async Task<IList<OnboardingPageListDto>> List()
    {
        return await service.ListAsync();
    }
}