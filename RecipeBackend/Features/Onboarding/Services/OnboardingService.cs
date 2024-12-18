using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeBackend.Core;
using RecipeBackend.Features.Onboarding.DTOs;
using RecipeBackend.Features.Onboarding.Models;
using RecipeBackend.Features.Onboarding.Repositories;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Features.Onboarding.Services;

public class OnboardingService(OnboardingRepository repo, IMapper mapper, IWebHostEnvironment wEnv, IHttpContextAccessor httpContext) : ServiceBase("onboarding_pages", wEnv)
{
    public async Task<OnboardingPage> CreateAsync([FromForm] OnboardingPageCreateDto payload)
    {
        payload.Order = await GetProperOrderAsync(payload.Order);
        var newOnboardingPage = mapper.Map<OnboardingPage>(payload);

        var fileName = await SaveUploadsFileAsync(payload.Picture);
        newOnboardingPage.Picture = fileName;

        return await repo.CreateAsync(newOnboardingPage);
    }

    public async Task<IList<OnboardingPage>> ListAsync()
    {
        ArgumentNullException.ThrowIfNull(httpContext.HttpContext);
        var onboardingPages = await repo.ListAsync();

        foreach (var onboardingPage in onboardingPages)
        {
            onboardingPage.Picture = httpContext.HttpContext.GetUploadsBaseUrl() + '/' + onboardingPage.Picture;
        }
        
        return onboardingPages;
    }

    // public async Task<OnboardingPage> UpdateOnboardingPageAsync(int id, OnboardingPageUpdateDto payload)
    // {
    //     var onboardingPage = await repo.GetByIdAsync(id);
    //
    //     DoesNotExistException.ThrowIfNull(onboardingPage, $"{nameof(OnboardingPage)} with {nameof(OnboardingPage.Id)}: {id} does not exist.");
    //
    //     if (payload.Order != null)
    //     {
    //         onboardingPage.Order = (int)payload.Order;
    //     }
    //
    //     if (payload.Title != null)
    //     {
    //         onboardingPage.Title = payload.Title;
    //     }
    //
    //     if (payload.Subtitle != null)
    //     {
    //         onboardingPage.Subtitle = payload.Subtitle;
    //     }
    //     
    //     
    //     
    // }

    private async Task<int> GetProperOrderAsync(int? order)
    {
        if (order is null or <= 0)
        {
            return await repo.GetMaxOrderAsync() + 1;
        }

        var orderExists = await repo.DoesOrderExistAsync((int)order);

        if (!orderExists)
        {
            int maxOrder = await repo.GetMaxOrderAsync() + 1;
            return maxOrder;
        }

        await repo.IncrementOrdersFrom((int)order);
        return (int)order;
    }
}