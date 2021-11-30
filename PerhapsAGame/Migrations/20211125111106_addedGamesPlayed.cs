using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerhapsAGame.Migrations
{
    public partial class addedGamesPlayed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Game",
                table: "Scores",
                newName: "GameName");

            migrationBuilder.AddColumn<int>(
                name: "GamesPlayed",
                table: "Scores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamesPlayed",
                table: "Scores");

            migrationBuilder.RenameColumn(
                name: "GameName",
                table: "Scores",
                newName: "Game");
        }
    }
}
