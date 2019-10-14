using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class LetsSeeAgain6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "Author",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MasterOfAllArtworksId",
                table: "Artwork",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterOfAllArtworks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterOfAllArtworks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_MasterId",
                table: "Author",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_MasterOfAllArtworksId",
                table: "Artwork",
                column: "MasterOfAllArtworksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artwork_MasterOfAllArtworks_MasterOfAllArtworksId",
                table: "Artwork",
                column: "MasterOfAllArtworksId",
                principalTable: "MasterOfAllArtworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_MasterOfAllArtworks_MasterId",
                table: "Author",
                column: "MasterId",
                principalTable: "MasterOfAllArtworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_MasterOfAllArtworks_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_MasterOfAllArtworks_MasterId",
                table: "Author");

            migrationBuilder.DropTable(
                name: "MasterOfAllArtworks");

            migrationBuilder.DropIndex(
                name: "IX_Author_MasterId",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Artwork_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "MasterOfAllArtworksId",
                table: "Artwork");
        }
    }
}
