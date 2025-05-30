using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Configurations;

public class CookingLevelConfigurations : IEntityTypeConfiguration<CookingLevel>
{
  public void Configure(EntityTypeBuilder<CookingLevel> builder)
  {
    builder.ToTable("cooking_levels");
    
    builder.HasKey(c => c.Id);

    builder.Property(c => c.Id)
      .HasColumnName("id");

    builder.Property(c => c.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(32);
    
    builder.Property(c => c.Description)
      .HasColumnName("description")
      .IsRequired()
      .HasMaxLength(512);
    
    builder.Property(c => c.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(c => c.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}