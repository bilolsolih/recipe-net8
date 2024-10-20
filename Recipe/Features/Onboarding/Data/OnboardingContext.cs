using Microsoft.EntityFrameworkCore;
using Recipe.Features.Onboarding.Configurations;
using Recipe.Features.Onboarding.Models;

namespace Recipe.Features.Onboarding.Data;

public class OnboardingContext(DbContextOptions<OnboardingContext> options) : DbContext(options)
{
    public DbSet<OnboardingPage> OnboardingPages { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OnboardingConfigurations());
    }
}