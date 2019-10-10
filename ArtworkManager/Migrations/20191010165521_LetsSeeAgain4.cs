using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class LetsSeeAgain4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_OwnerId",
                table: "User",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Author_OwnerId",
                table: "User",
                column: "OwnerId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Author_OwnerId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_OwnerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "User");
        }
    }
}
