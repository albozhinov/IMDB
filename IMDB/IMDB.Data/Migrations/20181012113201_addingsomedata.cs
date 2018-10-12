using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.Data.Migrations
{
    public partial class addingsomedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Ruben Fleischer" },
                    { 2, "Bradley Cooper" },
                    { 3, "Jeremy Saulnier" },
                    { 4, "Todd Phillips" },
                    { 5, "Paul Feig" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "ID", "GenreType" },
                values: new object[,]
                {
                    { 16, "Saga" },
                    { 18, "Sci-Fi" },
                    { 19, "Social" },
                    { 20, "Speculative" },
                    { 21, "Thriller" },
                    { 24, "Animation" },
                    { 23, "Western" },
                    { 15, "Romance" },
                    { 25, "Live-action scripted" },
                    { 26, "Live-action unscripted" },
                    { 27, "Crime" },
                    { 22, "Urban" },
                    { 14, "Political" },
                    { 17, "Satire" },
                    { 12, "Paranoid Fiction" },
                    { 13, "Philosophical" },
                    { 2, "Adventure" },
                    { 3, "Comedy" },
                    { 4, "Crime" },
                    { 5, "Drama" },
                    { 6, "Fantasy" },
                    { 1, "Action" },
                    { 8, "Historical fiction" },
                    { 9, "Horror" },
                    { 10, "Magical realism" },
                    { 11, "Mystery" },
                    { 7, "Historical" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "ID", "Rank", "Text" },
                values: new object[,]
                {
                    { 10, 2, "createmovie" },
                    { 9, 2, "deletemovie" },
                    { 8, 1, "deletereview" },
                    { 7, 1, "ratereview" },
                    { 6, 1, "ratemovie" },
                    { 4, 0, "searchmovie" },
                    { 3, 0, "checkmovie" },
                    { 2, 0, "login" },
                    { 1, 0, "register" },
                    { 5, 0, "showreviews" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Password", "Rank", "UserName" },
                values: new object[,]
                {
                    { 1, "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", 2, "admin" },
                    { 2, "325E7C598DE838FD1438C818A75E703FDDF7BA91C922EB68F2E0239705DFFA79", 1, "pesho" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "ID", "DirectorID", "IsDeleted", "MovieScore", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, 9.9, "Venom" },
                    { 2, 2, false, 0.0, "A Star Is Born" },
                    { 3, 3, false, 0.0, "Hold the Dark" },
                    { 4, 4, false, 9.0, "Joker" },
                    { 5, 5, false, 8.0, "A Simple Favor" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenreID", "MovieID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 11, 5 },
                    { 5, 5 },
                    { 4, 5 },
                    { 3, 5 },
                    { 5, 4 },
                    { 4, 4 },
                    { 21, 5 },
                    { 21, 3 },
                    { 5, 3 },
                    { 2, 3 },
                    { 15, 2 },
                    { 5, 2 },
                    { 18, 1 },
                    { 11, 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ID", "IsDeleted", "MovieID", "MovieRating", "ReviewScore", "Text", "UserID" },
                values: new object[,]
                {
                    { 4, false, 4, 9.0, 5.0, "evalata", 1 },
                    { 2, false, 1, 9.8, 9.0, "ba i qkiq film, a sym samo user", 2 },
                    { 1, false, 1, 10.0, 8.0, "Mn qko piche", 1 },
                    { 3, false, 5, 8.0, 0.0, "mn sex", 1 }
                });

            migrationBuilder.InsertData(
                table: "ReviewRatings",
                columns: new[] { "ID", "ReviewId", "ReviewRating", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 10.0, 1 },
                    { 2, 1, 6.0, 2 },
                    { 3, 2, 9.0, 1 },
                    { 4, 3, 5.0, 1 },
                    { 5, 3, 5.0, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 21, 3 });

            migrationBuilder.DeleteData(
                table: "MovieGenres",
                keyColumns: new[] { "GenreID", "MovieID" },
                keyValues: new object[] { 21, 5 });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ReviewRatings",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReviewRatings",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReviewRatings",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReviewRatings",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReviewRatings",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "ID",
                keyValue: 5);
        }
    }
}
