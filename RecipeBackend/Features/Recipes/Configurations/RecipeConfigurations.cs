using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class RecipeConfigurations : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable(
            "Recipe",
            t => t.HasCheckConstraint("CK_TimeRequired", "\"TimeRequired\" > 0")
        );

        builder.HasKey(r => r.Id);
        builder.HasOne(r => r.User).WithMany(u => u.Recipes);
        builder.HasOne(r => r.Category).WithMany(c => c.Recipes);
        
        builder.Property(r => r.Title).HasMaxLength(64).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(1024).IsRequired();
        builder.Property(r => r.Photo).IsRequired();
        builder.Property(r => r.VideoRecipe).IsRequired(false);
        
        builder.Property(r => r.Created)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAdd();

        builder.Property(r => r.Updated)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAddOrUpdate();
        
    }
}