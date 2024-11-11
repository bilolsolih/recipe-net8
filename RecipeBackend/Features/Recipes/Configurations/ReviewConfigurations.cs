using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Authentication.Models;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class ReviewConfigurations : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Review", t => t.HasCheckConstraint("CK_Rating", "\"Rating\" >= 1 AND \"Rating\" <= 5"));
        builder.HasOne<User>().WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        builder.HasOne<Recipe>().WithMany(r => r.Reviews).HasForeignKey(r => r.RecipeId);

        builder.HasIndex(r => new { r.UserId, r.RecipeId }).IsUnique();
        builder.Property(r => r.Comment).IsRequired().HasMaxLength(512);
        
        builder.Property(r => r.Created)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAdd();

        builder.Property(r => r.Updated)
               .HasDefaultValueSql("CURRENT_TIMESTAMP")
               .ValueGeneratedOnAddOrUpdate();
    }
}