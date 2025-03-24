using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OnboardingPage",
                table: "OnboardingPage");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "OnboardingPage");

            migrationBuilder.RenameTable(
                name: "OnboardingPage",
                newName: "onboarding_pages");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "onboarding_pages",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "onboarding_pages",
                newName: "subtitle");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "onboarding_pages",
                newName: "order");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "onboarding_pages",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_onboarding_pages",
                table: "onboarding_pages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "notification_icons",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    icon = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_icons", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_icons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_onboarding_pages",
                table: "onboarding_pages");

            migrationBuilder.DropColumn(
                name: "image",
                table: "onboarding_pages");

            migrationBuilder.RenameTable(
                name: "onboarding_pages",
                newName: "OnboardingPage");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "OnboardingPage",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "subtitle",
                table: "OnboardingPage",
                newName: "Subtitle");

            migrationBuilder.RenameColumn(
                name: "order",
                table: "OnboardingPage",
                newName: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "OnboardingPage",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnboardingPage",
                table: "OnboardingPage",
                column: "Id");
        }
    }
}
