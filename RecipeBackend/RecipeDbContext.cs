using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Authentication.Configurations;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Customization.Configurations;
using RecipeBackend.Features.Customization.Models;
using RecipeBackend.Features.Notifications.Configurations;
using RecipeBackend.Features.Notifications.Models;
using RecipeBackend.Features.Onboarding.Configurations;
using RecipeBackend.Features.Onboarding.Models;
using RecipeBackend.Features.Recipes.Configurations;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend;

public class RecipeDbContext(DbContextOptions<RecipeDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<UserToUser> UsersToUsers { get; set; }
  public DbSet<CookingLevel> CookingLevels { get; set; }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Recipe> Recipes { get; set; }
  public DbSet<Ingredient> Ingredients { get; set; }
  public DbSet<Instruction> Instructions { get; set; }
  public DbSet<Review> Reviews { get; set; }

  public DbSet<OnboardingPage> OnboardingPages { get; set; }
  public DbSet<AllergicIngredient> AllergicIngredients { get; set; }
  public DbSet<Cuisine> Cuisines { get; set; }

  public DbSet<NotificationIcon> NotificationIcons { get; set; }
  public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
  public DbSet<Notification> Notifications { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new UserConfigurations());
    // modelBuilder.ApplyConfiguration(new UserToUserConfigurations());
    modelBuilder.ApplyConfiguration(new CookingLevelConfigurations());

    modelBuilder.ApplyConfiguration(new CategoryConfigurations());
    modelBuilder.ApplyConfiguration(new RecipeConfigurations());
    modelBuilder.ApplyConfiguration(new IngredientConfigurations());
    modelBuilder.ApplyConfiguration(new InstructionConfigurations());
    modelBuilder.ApplyConfiguration(new ReviewConfigurations());

    modelBuilder.ApplyConfiguration(new OnboardingConfigurations());
    modelBuilder.ApplyConfiguration(new AllergicIngredientConfigurations());
    modelBuilder.ApplyConfiguration(new CuisineConfigurations());

    modelBuilder.ApplyConfiguration(new NotificationIconConfigurations());
    modelBuilder.ApplyConfiguration(new NotificationTemplateConfigurations());
    modelBuilder.ApplyConfiguration(new NotificationConfigurations());
  }
}