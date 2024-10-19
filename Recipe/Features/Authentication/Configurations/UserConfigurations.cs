using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipe.Features.Authentication.Models;

namespace Recipe.Features.Authentication.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username)
               .IsRequired()
               .HasMaxLength(64);
        
        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(64);
        
        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(64);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(64);

        builder.Property(u => u.Password)
               .IsRequired();

        builder.HasIndex(u => u.Username)
               .IsUnique();

        builder.HasIndex(u => u.Email)
               .IsUnique();
    }
}