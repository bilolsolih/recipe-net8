using Microsoft.AspNetCore.Mvc;
using Recipe.Features.Onboarding.Models;
using Recipe.Features.Onboarding.Services;
using Recipe.Features.Onboarding.DTOs;

namespace Recipe.Features.Onboarding.Controllers;

[ApiController, Route("api/v1/onboarding")]
public class OnboardingController(OnboardingService service) : ControllerBase
{
    [HttpPost("create")]
    public OnboardingPage Create(OnboardingPageCreateDto payload)
    {
        return service.Create(payload);
    }

    [HttpGet("list")]
    public IList<OnboardingPageListDto> List()
    {
        return service.List();
    }
}