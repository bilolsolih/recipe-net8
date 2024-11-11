using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class IngredientConfigurations : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("Ingredient");
        builder.HasKey(i => i.Id);
        
        builder.HasOne(i => i.Recipe).WithMany(r => r.Ingredients);
        builder.Property(i => i.Amount).IsRequired();
        builder.Property(i => i.Name).IsRequired();

        builder.Property(i => i.Created)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAdd();

        builder.Property(i => i.Updated)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAddOrUpdate();
    }
}