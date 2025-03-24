using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Configurations;

public class NotificationIconConfigurations : IEntityTypeConfiguration<NotificationIcon>
{
  public void Configure(EntityTypeBuilder<NotificationIcon> builder)
  {
    builder.ToTable("notification_icons");
    builder.HasKey(notification => notification.Id);

    builder.Property(notification => notification.Id)
      .HasColumnName("id");
    
    builder.Property(notification => notification.Title)
      .IsRequired()
      .HasMaxLength(64)
      .HasColumnName("title");
    
    builder.Property(notification => notification.Icon)
      .IsRequired()
      .HasMaxLength(64)
      .HasColumnName("icon");
    
    builder.Property(notification => notification.Created)
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .HasColumnName("created");
    
    builder.Property(notification => notification.Updated)
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .HasColumnName("updated");
  }
}