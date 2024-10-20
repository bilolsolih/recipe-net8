using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Onboarding.Configurations;
using RecipeBackend.Features.Onboarding.Models;

namespace RecipeBackend.Features.Onboarding.Data;

public class OnboardingContext(DbContextOptions<OnboardingContext> options) : DbContext(options)
{
    public DbSet<OnboardingPage> OnboardingPages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OnboardingConfigurations());
    }
}