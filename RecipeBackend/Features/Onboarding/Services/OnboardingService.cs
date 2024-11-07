using AutoMapper;
using RecipeBackend.Features.Onboarding.DTOs;
using RecipeBackend.Features.Onboarding.Models;
using RecipeBackend.Features.Onboarding.Repositories;

namespace RecipeBackend.Features.Onboarding.Services;

public class OnboardingService(OnboardingRepository repository, IMapper mapper)
{
    public async Task<OnboardingPage> CreateAsync(OnboardingPageCreateDto payload)
    {
        payload.Order = await GetProperOrderAsync(payload.Order);
        var newOnboardingPage = mapper.Map<OnboardingPage>(payload);
        return await repository.CreateAsync(newOnboardingPage);
    }

    public async Task<IList<OnboardingPageListDto>> ListAsync()
    {
        var onboardingPages = await repository.ListAsync();
        var onboardingPagesList = mapper.Map<IList<OnboardingPageListDto>>(onboardingPages);
        return onboardingPagesList;
    }

    private async Task<int> GetProperOrderAsync(int? order)
    {
        if (order is null or <= 0)
        {
            return await repository.GetMaxOrderAsync() + 1;
        }

        var orderExists = await repository.DoesOrderExistAsync((int)order);

        if (!orderExists)
        {
            int maxOrder = await repository.GetMaxOrderAsync() + 1;
            return maxOrder;
        }

        await repository.IncrementOrdersFrom((int)order);
        return (int)order;
    }
}