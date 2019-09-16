using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class LetsSeeAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_Author_OwnerId",
                table: "Artwork");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Artwork",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Artwork_OwnerId",
                table: "Artwork",
                newName: "IX_Artwork_OwnerID");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "Artwork",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artwork_Author_OwnerID",
                table: "Artwork",
                column: "OwnerID",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_Author_OwnerID",
                table: "Artwork");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Artwork",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Artwork_OwnerID",
                table: "Artwork",
                newName: "IX_Artwork_OwnerId");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Artwork",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Artwork_Author_OwnerId",
                table: "Artwork",
                column: "OwnerId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
