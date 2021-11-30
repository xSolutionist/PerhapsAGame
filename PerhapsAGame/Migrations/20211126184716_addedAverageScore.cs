using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerhapsAGame.Migrations
{
    public partial class addedAverageScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageScore",
                table: "Scores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageScore",
                table: "Scores");
        }
    }
}
