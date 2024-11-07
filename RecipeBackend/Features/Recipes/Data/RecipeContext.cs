using Microsoft.EntityFrameworkCore;
using RecipeBackend.Features.Recipes.Configurations;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Data;

public class RecipeContext(DbContextOptions<RecipeContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoryConfigurations());
    }
}