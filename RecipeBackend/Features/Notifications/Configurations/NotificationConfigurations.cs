using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Configurations;

public class NotificationConfigurations : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("notifications");
    builder.HasKey(notification => notification.Id);

    builder.HasOne(notification => notification.NotificationTemplate)
      .WithMany()
      .HasForeignKey(notification => notification.NotificationTemplateId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(notification => notification.Id)
      .HasColumnName("id");

    builder.Property(notification => notification.NotificationTemplateId)
      .HasColumnName("notification_template_id");
    
    builder.Property(notification => notification.ScheduledDate)
      .IsRequired()
      .HasColumnName("scheduled_date");

    builder.Property(notification => notification.SendNow)
      .IsRequired()
      .HasColumnName("send_now");

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