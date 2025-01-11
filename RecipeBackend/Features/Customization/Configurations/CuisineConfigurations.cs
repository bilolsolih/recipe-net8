using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Customization.Models;

namespace RecipeBackend.Features.Customization.Configurations;

public class CuisineConfigurations : IEntityTypeConfiguration<Cuisine>
{
    public void Configure(EntityTypeBuilder<Cuisine> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title).HasMaxLength(16).IsRequired();
        builder.Property(a => a.Image).IsRequired();
    }
}