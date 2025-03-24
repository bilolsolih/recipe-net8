using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Recipes.Models;

namespace RecipeBackend.Features.Recipes.Configurations;

public class CollectionConfigurations : IEntityTypeConfiguration<Collection>
{
  public void Configure(EntityTypeBuilder<Collection> builder)
  {
    builder.ToTable("collections");
    builder.HasKey(collection => collection.Id);

    builder.HasIndex(collection => new { collection.UserId, collection.Title })
      .IsUnique();

    builder.Property(collection => collection.Id).HasColumnName("id");
    builder.Property(collection => collection.Id).HasColumnName("id");
    builder.Property(collection => collection.Id).HasColumnName("id");
    builder.Property(collection => collection.Id).HasColumnName("id");
    
    



  }
}