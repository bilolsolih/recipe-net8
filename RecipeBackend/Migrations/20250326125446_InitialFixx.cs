using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialFixx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_to_users_Users_FollowerId",
                table: "users_to_users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_to_users_Users_FollowersId",
                table: "users_to_users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_to_users_Users_FollowingsId",
                table: "users_to_users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_to_users_Users_UserId",
                table: "users_to_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users_to_users",
                table: "users_to_users");

            migrationBuilder.DropIndex(
                name: "IX_users_to_users_FollowersId",
                table: "users_to_users");

            migrationBuilder.DropIndex(
                name: "IX_users_to_users_FollowingsId",
                table: "users_to_users");

            migrationBuilder.DropColumn(
                name: "FollowersId",
                table: "users_to_users");

            migrationBuilder.DropColumn(
                name: "FollowingsId",
                table: "users_to_users");

            migrationBuilder.RenameTable(
                name: "users_to_users",
                newName: "UsersToUsers");

            migrationBuilder.RenameIndex(
                name: "IX_users_to_users_FollowerId",
                table: "UsersToUsers",
                newName: "IX_UsersToUsers_FollowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersToUsers",
                table: "UsersToUsers",
                columns: new[] { "UserId", "FollowerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToUsers_Users_FollowerId",
                table: "UsersToUsers",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToUsers_Users_UserId",
                table: "UsersToUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersToUsers_Users_FollowerId",
                table: "UsersToUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersToUsers_Users_UserId",
                table: "UsersToUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersToUsers",
                table: "UsersToUsers");

            migrationBuilder.RenameTable(
                name: "UsersToUsers",
                newName: "users_to_users");

            migrationBuilder.RenameIndex(
                name: "IX_UsersToUsers_FollowerId",
                table: "users_to_users",
                newName: "IX_users_to_users_FollowerId");

            migrationBuilder.AddColumn<int>(
                name: "FollowersId",
                table: "users_to_users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowingsId",
                table: "users_to_users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users_to_users",
                table: "users_to_users",
                columns: new[] { "UserId", "FollowerId" });

            migrationBuilder.CreateIndex(
                name: "IX_users_to_users_FollowersId",
                table: "users_to_users",
                column: "FollowersId");

            migrationBuilder.CreateIndex(
                name: "IX_users_to_users_FollowingsId",
                table: "users_to_users",
                column: "FollowingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_to_users_Users_FollowerId",
                table: "users_to_users",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_to_users_Users_FollowersId",
                table: "users_to_users",
                column: "FollowersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_to_users_Users_FollowingsId",
                table: "users_to_users",
                column: "FollowingsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_to_users_Users_UserId",
                table: "users_to_users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
