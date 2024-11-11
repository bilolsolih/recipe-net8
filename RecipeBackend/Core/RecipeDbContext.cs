using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Authentication.Configurations;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Onboarding.Configurations;
using RecipeBackend.Features.Onboarding.Models;
using RecipeBackend.Features.Recipes.Configurations;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Core;

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    
    public DbSet<OnboardingPage> OnboardingPages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        
        modelBuilder.ApplyConfiguration(new CategoryConfigurations());
        modelBuilder.ApplyConfiguration(new RecipeConfigurations());
        modelBuilder.ApplyConfiguration(new IngredientConfigurations());
        modelBuilder.ApplyConfiguration(new InstructionConfigurations());
        
        modelBuilder.ApplyConfiguration(new OnboardingConfigurations());
    }
}