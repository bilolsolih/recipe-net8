using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Onboarding.Data;
using RecipeBackend.Features.Onboarding.Models;

namespace RecipeBackend.Features.Onboarding.Repositories;

public class OnboardingRepository(OnboardingContext context)
{
    public async Task<OnboardingPage> CreateAsync(OnboardingPage onboardingPage)
    {
        context.OnboardingPages.Add(onboardingPage);
        await context.SaveChangesAsync();
        return onboardingPage;
    }

    public async Task<IList<OnboardingPage>> ListAsync()
    {
        var onboardingPages = await context.OnboardingPages.ToListAsync();
        return onboardingPages;
    }

    public async Task<int> GetMaxOrderAsync()
    {
        var maxOrder = await context.OnboardingPages.MaxAsync(o => (int?)o.Order) ?? 0;
        return maxOrder;
    }

    public async Task<bool> DoesOrderExistAsync(int order)
    {
        return await context.OnboardingPages.AnyAsync(o => o.Order == order);
    }

    public async Task IncrementOrdersFrom(int start)
    {
        await context.Database.ExecuteSqlRawAsync("""UPDATE "OnboardingPage" SET "Order" = "Order" + 1 WHERE "Order" >= {0}""", start);
    }
}