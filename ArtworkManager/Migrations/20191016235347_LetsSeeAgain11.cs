using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class LetsSeeAgain11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Master_MasterId",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_MasterId",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "MasterId",
                table: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MasterId",
                table: "Author",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_MasterId",
                table: "Author",
                column: "MasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Master_MasterId",
                table: "Author",
                column: "MasterId",
                principalTable: "Master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
