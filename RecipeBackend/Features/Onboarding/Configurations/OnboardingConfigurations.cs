using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Onboarding.Models;

namespace RecipeBackend.Features.Onboarding.Configurations;

public class OnboardingConfigurations : IEntityTypeConfiguration<OnboardingPage>
{
  public void Configure(EntityTypeBuilder<OnboardingPage> builder)
  {
    builder.ToTable("onboarding_pages");
    builder.HasKey(o => o.Id);

    builder.Property(o => o.Title)
      .IsRequired()
      .HasMaxLength(128)
      .HasColumnName("title");

    builder.Property(o => o.Subtitle)
      .IsRequired()
      .HasMaxLength(256)
      .HasColumnName("subtitle");

    builder.Property(o => o.Image)
      .IsRequired()
      .HasMaxLength(128)
      .HasColumnName("image");

    builder.Property(o => o.Order)
      .IsRequired()
      .HasColumnName("order");
  }
}