using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Features.Authentication.Models;

namespace Recipe.Features.Authentication.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.Property(u => u.FullName)
               .IsRequired()
               .HasMaxLength(64);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(64);

        builder.Property(u => u.PhoneNumber)
               .IsRequired()
               .HasMaxLength(16);

        builder.Property(u => u.BirthDate)
               .IsRequired();

        builder.Property(u => u.Password)
               .IsRequired();

        builder.Property(u => u.Gender)
               .IsRequired(false);

        builder.Property(u => u.ProfilePhoto)
               .IsRequired(false);
    }
}