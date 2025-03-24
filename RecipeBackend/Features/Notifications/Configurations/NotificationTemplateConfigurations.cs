using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBackend.Features.Notifications.Models;

namespace RecipeBackend.Features.Notifications.Configurations;

public class NotificationTemplateConfigurations : IEntityTypeConfiguration<NotificationTemplate>
{
  public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
  {
    builder.ToTable("notification_templates");
    builder.HasKey(template => template.Id);

    builder.HasOne(template => template.NotificationIcon)
      .WithMany()
      .HasForeignKey(template => template.NotificationIconId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasIndex(template => template.Title)
      .IsUnique();

    builder.Property(template => template.Id)
      .HasColumnName("id");

    builder.Property(template => template.NotificationIconId)
      .HasColumnName("notification_icon_id");

    builder.Property(template => template.Title)
      .IsRequired()
      .HasMaxLength(64)
      .HasColumnName("title");

    builder.Property(template => template.Subtitle)
      .IsRequired()
      .HasMaxLength(128)
      .HasColumnName("subtitle");

    builder.Property(template => template.Created)
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .HasColumnName("created");

    builder.Property(template => template.Updated)
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd()
      .HasColumnName("updated");
  }
}