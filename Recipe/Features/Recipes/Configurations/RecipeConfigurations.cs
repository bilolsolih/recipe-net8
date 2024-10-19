using Microsoft.EntityFrameworkCore;
using RecipeEntity = Recipe.Features.Recipes.Models.Recipe;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipe.Features.Recipes.Configurations;

public class RecipeConfigurations : IEntityTypeConfiguration<RecipeEntity>
{
    public void Configure(EntityTypeBuilder<RecipeEntity> builder)
    {
    }
}
