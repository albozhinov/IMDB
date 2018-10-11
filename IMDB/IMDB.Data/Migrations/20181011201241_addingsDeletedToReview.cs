using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.Data.Migrations
{
    public partial class addingsDeletedToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MovieGenreID",
                table: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieGenreID",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }
    }
}
