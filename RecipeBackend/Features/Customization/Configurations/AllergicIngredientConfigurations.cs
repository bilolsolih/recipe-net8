using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Customization.Models;

namespace RecipeBackend.Features.Customization.Configurations;

public class AllergicIngredientConfigurations : IEntityTypeConfiguration<AllergicIngredient>
{
    public void Configure(EntityTypeBuilder<AllergicIngredient> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title).HasMaxLength(16).IsRequired();
        builder.Property(a => a.Image).IsRequired();
    }
}