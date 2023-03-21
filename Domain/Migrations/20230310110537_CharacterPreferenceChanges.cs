using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class CharacterPreferenceChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clothes",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Decor",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Hobby",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clothes",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Decor",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hobby",
                table: "Characters",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Music",
                table: "Characters",
                type: "integer",
                nullable: true);
        }
    }
}
