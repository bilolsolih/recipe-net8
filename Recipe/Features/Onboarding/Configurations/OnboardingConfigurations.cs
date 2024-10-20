using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Features.Onboarding.Models;

namespace Recipe.Features.Onboarding.Configurations;

public class OnboardingConfigurations : IEntityTypeConfiguration<OnboardingPage>
{
    public void Configure(EntityTypeBuilder<OnboardingPage> builder)
    {
        builder.ToTable("OnboardingPage");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Title)
               .IsRequired()
               .HasMaxLength(128);

        builder.Property(o => o.Subtitle)
               .IsRequired()
               .HasMaxLength(256);

        builder.Property(o => o.Picture)
               .IsRequired();

        builder.Property(o => o.Order)
               .IsRequired();
    }
}