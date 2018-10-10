using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.Data.Migrations
{
    public partial class fullDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRatings",
                table: "ReviewRatings");

            migrationBuilder.DropIndex(
                name: "IX_ReviewRatings_UserId",
                table: "ReviewRatings");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ReviewRatings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRatings",
                table: "ReviewRatings",
                columns: new[] { "UserId", "ReviewId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewRatings",
                table: "ReviewRatings");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ReviewRatings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewRatings",
                table: "ReviewRatings",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRatings_UserId",
                table: "ReviewRatings",
                column: "UserId");
        }
    }
}
