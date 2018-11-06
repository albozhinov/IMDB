using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDB.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    UserID = table.Column<string>(nullable: false),
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
                        name: "FK_Reviews_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewRatings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
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
                        name: "FK_ReviewRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5197310d-5d42-4337-bb59-2fd06e6a8fcd", "a3bc9d45-276b-442f-bc6b-b1a5763df30d", "User", "USER" },
                    { "959596e5-93e4-4272-8cfb-6e71a4254370", "20d35162-b35c-4b2e-80c1-81a15bc1b2f3", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3ccbce59-fa31-4292-900c-b9f927c3bae6", 0, "8e38f2f1-5f7a-465c-9bab-9a552ce12cd3", "pesho@porn.bg", false, false, null, "PESHO@PORN.BG", "PESHO@PORN.BG", "AQAAAAEAACcQAAAAEJt8CwoO+qkgaaa/l+VY1EVz+CH0be0kXBHROfTYZQ38MHV5JMTl25QhQwYoc4CpeQ==", null, false, "MR4D3LBVUTWTFR567JX6IVOKN37YDNIX", false, "pesho@porn.bg" },
                    { "676ce4b1-6641-4909-a478-11b173180b3c", 0, "13e8f9aa-0f6b-46e8-acd1-65dd9ceffe17", "admin@porn.bg", false, false, null, "ADMIN@PORN.BG", "ADMIN@PORN.BG", "AQAAAAEAACcQAAAAELA1iavMGFwvWsLgwRbMIKvkYmxTxUxJEWgQKTJnyYaSyDidI9/FVdDg3mbqXB++Fg==", null, false, "NL3NHSV53KAQ25IHMAR6B42WGI53HKCY", false, "admin@porn.bg" }
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
                    { 1, "Action" },
                    { 27, "Crime" },
                    { 26, "Live-action unscripted" },
                    { 25, "Live-action scripted" },
                    { 24, "Animation" },
                    { 23, "Western" },
                    { 22, "Urban" },
                    { 21, "Thriller" },
                    { 20, "Speculative" },
                    { 19, "Social" },
                    { 18, "Sci-Fi" },
                    { 17, "Satire" },
                    { 16, "Saga" },
                    { 14, "Political" },
                    { 12, "Paranoid Fiction" },
                    { 11, "Mystery" },
                    { 10, "Magical realism" },
                    { 9, "Horror" },
                    { 8, "Historical fiction" },
                    { 7, "Historical" },
                    { 6, "Fantasy" },
                    { 5, "Drama" },
                    { 4, "Crime" },
                    { 3, "Comedy" },
                    { 2, "Adventure" },
                    { 15, "Romance" },
                    { 13, "Philosophical" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "676ce4b1-6641-4909-a478-11b173180b3c", "959596e5-93e4-4272-8cfb-6e71a4254370" },
                    { "3ccbce59-fa31-4292-900c-b9f927c3bae6", "5197310d-5d42-4337-bb59-2fd06e6a8fcd" }
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
                    { 4, false, 4, 9.0, 0, 5.0, "evalata", "676ce4b1-6641-4909-a478-11b173180b3c" },
                    { 2, false, 1, 9.8, 1, 9.0, "ba i qkiq film, a sym samo user", "3ccbce59-fa31-4292-900c-b9f927c3bae6" },
                    { 1, false, 1, 10.0, 2, 8.0, "Mn qko piche", "676ce4b1-6641-4909-a478-11b173180b3c" },
                    { 3, false, 5, 8.0, 2, 0.0, "mn sex", "676ce4b1-6641-4909-a478-11b173180b3c" }
                });

            migrationBuilder.InsertData(
                table: "ReviewRatings",
                columns: new[] { "ID", "ReviewId", "ReviewRating", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 10.0, "676ce4b1-6641-4909-a478-11b173180b3c" },
                    { 2, 1, 6.0, "3ccbce59-fa31-4292-900c-b9f927c3bae6" },
                    { 3, 2, 9.0, "676ce4b1-6641-4909-a478-11b173180b3c" },
                    { 4, 3, 5.0, "676ce4b1-6641-4909-a478-11b173180b3c" },
                    { 5, 3, 5.0, "3ccbce59-fa31-4292-900c-b9f927c3bae6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "ReviewRatings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
