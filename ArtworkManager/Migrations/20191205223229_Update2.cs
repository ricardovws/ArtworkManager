using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Artwork");

            migrationBuilder.AddColumn<bool>(
                name: "TypeOfArtwork",
                table: "Artwork",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfArtwork",
                table: "Artwork");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Artwork",
                nullable: false,
                defaultValue: 0);
        }
    }
}
