using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtworkManager.Migrations
{
    public partial class TeamIdForeighKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artwork_Master_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropForeignKey(
                name: "FK_Author_Team_TeamId",
                table: "Author");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropIndex(
                name: "IX_Artwork_MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.DropColumn(
                name: "MasterOfAllArtworksId",
                table: "Artwork");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Author",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Team_TeamId",
                table: "Author",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Team_TeamId",
                table: "Author");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Author",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MasterOfAllArtworksId",
                table: "Artwork",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_MasterOfAllArtworksId",
                table: "Artwork",
                column: "MasterOfAllArtworksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artwork_Master_MasterOfAllArtworksId",
                table: "Artwork",
                column: "MasterOfAllArtworksId",
                principalTable: "Master",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Team_TeamId",
                table: "Author",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
