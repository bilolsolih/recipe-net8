using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Authentication.Models;

namespace RecipeBackend.Features.Authentication.Configurations;

public class UserToUserConfigurations : IEntityTypeConfiguration<UserToUser>
{
  public void Configure(EntityTypeBuilder<UserToUser> builder)
  {
    builder.ToTable("users_to_users");

    builder.HasKey(u => new { u.UserId, u.FollowerId });

    builder.HasOne(u => u.User)
      .WithMany()
      .HasForeignKey(u => u.UserId)
      .OnDelete(DeleteBehavior.Cascade);
    
    builder.HasOne(u => u.Follower)
      .WithMany()
      .HasForeignKey(u => u.FollowerId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}