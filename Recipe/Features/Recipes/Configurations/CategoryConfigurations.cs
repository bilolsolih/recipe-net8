using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Features.Recipes.Models;

namespace Recipe.Features.Recipes.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .HasMaxLength(64)
               .IsRequired();

        builder.Property(c => c.Photo)
               .IsRequired();

        builder.HasIndex(c => c.Title)
               .IsUnique();
    }
}