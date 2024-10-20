using Microsoft.EntityFrameworkCore;
using RecipeEntity = RecipeBackend.Features.Recipes.Models.Recipe;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
    }
}
