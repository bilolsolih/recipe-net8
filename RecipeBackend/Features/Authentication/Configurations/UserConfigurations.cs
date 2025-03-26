using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder.HasMany(u => u.Followers)
      .WithMany(u=>u.Followings)
      .UsingEntity<UserToUser>(
        j => j.HasOne(uf => uf.Follower)
          .WithMany()
          .HasForeignKey(uf => uf.FollowerId)
          .OnDelete(DeleteBehavior.Restrict),
        j => j.HasOne(uf => uf.User)
          .WithMany()
          .HasForeignKey(uf => uf.UserId)
          .OnDelete(DeleteBehavior.Restrict),

        j => j.HasKey(uf => new { uf.UserId, uf.FollowerId })
      );

    builder.HasIndex(u => u.Email)
      .IsUnique();

    builder.HasIndex(u => u.Username)
      .IsUnique();

    builder.Property(u => u.Username)
      .IsRequired()
      .HasMaxLength(24);

    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(32);
    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(32);

    builder.Property(u => u.Email)
      .IsRequired()
      .HasMaxLength(64);

    builder.Property(u => u.Presentation)
      .IsRequired(false)
      .HasMaxLength(256);

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

    builder.Property(u => u.Created)
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(u => u.Updated)
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAddOrUpdate();
  }
}