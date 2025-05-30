using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBackend.Migrations
{
    /// <inheritdoc />
    public partial class MadeSomeNecessaryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookingLevelId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AllergicIngredientUser",
                columns: table => new
                {
                    AllergicIngredientsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergicIngredientUser", x => new { x.AllergicIngredientsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AllergicIngredientUser_AllergicIngredients_AllergicIngredientsId",
                        column: x => x.AllergicIngredientsId,
                        principalTable: "AllergicIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergicIngredientUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cooking_levels",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cooking_levels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CuisineUser",
                columns: table => new
                {
                    CuisinesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineUser", x => new { x.CuisinesId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CuisineUser_Cuisines_CuisinesId",
                        column: x => x.CuisinesId,
                        principalTable: "Cuisines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuisineUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CookingLevelId",
                table: "Users",
                column: "CookingLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergicIngredientUser_UserId",
                table: "AllergicIngredientUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CuisineUser_UserId",
                table: "CuisineUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_cooking_levels_CookingLevelId",
                table: "Users",
                column: "CookingLevelId",
                principalTable: "cooking_levels",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_cooking_levels_CookingLevelId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AllergicIngredientUser");

            migrationBuilder.DropTable(
                name: "cooking_levels");

            migrationBuilder.DropTable(
                name: "CuisineUser");

            migrationBuilder.DropIndex(
                name: "IX_Users_CookingLevelId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CookingLevelId",
                table: "Users");
        }
    }
}
