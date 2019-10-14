using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class LetsSeeAgain10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_MasterOfAllArtworks_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_MasterOfAllArtworks_MasterId",
                table: "Author");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MasterOfAllArtworks",
                table: "MasterOfAllArtworks");

            migrationBuilder.RenameTable(
                name: "MasterOfAllArtworks",
                newName: "Master");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Master",
                table: "Master",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artwork_Master_MasterOfAllArtworksId",
                table: "Artwork",
                column: "MasterOfAllArtworksId",
                principalTable: "Master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Master_MasterId",
                table: "Author",
                column: "MasterId",
                principalTable: "Master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_Master_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Master_MasterId",
                table: "Author");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Master",
                table: "Master");

            migrationBuilder.RenameTable(
                name: "Master",
                newName: "MasterOfAllArtworks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MasterOfAllArtworks",
                table: "MasterOfAllArtworks",
                column: "Id");

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
    }
}
