using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationTemplateSomechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_templates_notification_icons_NotificationIconId",
                table: "notification_templates");

            migrationBuilder.RenameColumn(
                name: "NotificationIconId",
                table: "notification_templates",
                newName: "notification_icon_id");

            migrationBuilder.RenameIndex(
                name: "IX_notification_templates_NotificationIconId",
                table: "notification_templates",
                newName: "IX_notification_templates_notification_icon_id");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_templates_notification_icons_notification_icon_id",
                table: "notification_templates",
                column: "notification_icon_id",
                principalTable: "notification_icons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notification_templates_notification_icons_notification_icon_id",
                table: "notification_templates");

            migrationBuilder.RenameColumn(
                name: "notification_icon_id",
                table: "notification_templates",
                newName: "NotificationIconId");

            migrationBuilder.RenameIndex(
                name: "IX_notification_templates_notification_icon_id",
                table: "notification_templates",
                newName: "IX_notification_templates_NotificationIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_notification_templates_notification_icons_NotificationIconId",
                table: "notification_templates",
                column: "NotificationIconId",
                principalTable: "notification_icons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
