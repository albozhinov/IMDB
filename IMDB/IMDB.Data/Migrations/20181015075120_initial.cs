using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 78, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GenreType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MovieScore = table.Column<double>(nullable: false),
                    NumberOfVotes = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DirectorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorID",
                        column: x => x.DirectorID,
                        principalTable: "Directors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false),
                    GenreID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.GenreID, x.MovieID });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MovieRating = table.Column<double>(nullable: false),
                    ReviewScore = table.Column<double>(nullable: false),
                    NumberOfVotes = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRatings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false),
                    ReviewRating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewRatings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReviewRatings_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    { 17, "Satire" },
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
                    { 18, "Sci-Fi" },
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
                    { 11, 2, "createmovie" },
                    { 10, 2, "deletemovie" },
                    { 9, 1, "logout" },
                    { 8, 1, "deletereview" },
                    { 7, 1, "ratereview" },
                    { 4, 0, "searchmovie" },
                    { 5, 0, "listmoviereviews" },
                    { 3, 0, "checkmovie" },
                    { 2, 0, "login" },
                    { 1, 0, "register" },
                    { 6, 1, "ratemovie" }
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
                columns: new[] { "ID", "DirectorID", "IsDeleted", "MovieScore", "Name", "NumberOfVotes" },
                values: new object[,]
                {
                    { 1, 1, false, 9.9, "Venom", 2 },
                    { 2, 2, false, 0.0, "A Star Is Born", 0 },
                    { 3, 3, false, 0.0, "Hold the Dark", 0 },
                    { 4, 4, false, 9.0, "Joker", 1 },
                    { 5, 5, false, 8.0, "A Simple Favor", 1 }
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
                columns: new[] { "ID", "IsDeleted", "MovieID", "MovieRating", "NumberOfVotes", "ReviewScore", "Text", "UserID" },
                values: new object[,]
                {
                    { 4, false, 4, 9.0, 0, 5.0, "evalata", 1 },
                    { 2, false, 1, 9.8, 1, 9.0, "ba i qkiq film, a sym samo user", 2 },
                    { 1, false, 1, 10.0, 2, 8.0, "Mn qko piche", 1 },
                    { 3, false, 5, 8.0, 2, 0.0, "mn sex", 1 }
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

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieID",
                table: "MovieGenres",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorID",
                table: "Movies",
                column: "DirectorID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRatings_ReviewId",
                table: "ReviewRatings",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewRatings_UserId",
                table: "ReviewRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieID",
                table: "Reviews",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ReviewRatings");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
