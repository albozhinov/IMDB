﻿// <auto-generated />
using System;
using IMDB.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMDB.Data.Migrations
{
    [DbContext(typeof(IMDBContext))]
    [Migration("20181029184809_intiial")]
    partial class intiial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMDB.Data.Models.Director", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(78);

                    b.HasKey("ID");

                    b.ToTable("Directors");

                    b.HasData(
                        new { ID = 1, Name = "Ruben Fleischer" },
                        new { ID = 2, Name = "Bradley Cooper" },
                        new { ID = 3, Name = "Jeremy Saulnier" },
                        new { ID = 4, Name = "Todd Phillips" },
                        new { ID = 5, Name = "Paul Feig" }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GenreType")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Genres");

                    b.HasData(
                        new { ID = 1, GenreType = "Action" },
                        new { ID = 2, GenreType = "Adventure" },
                        new { ID = 3, GenreType = "Comedy" },
                        new { ID = 4, GenreType = "Crime" },
                        new { ID = 5, GenreType = "Drama" },
                        new { ID = 6, GenreType = "Fantasy" },
                        new { ID = 7, GenreType = "Historical" },
                        new { ID = 8, GenreType = "Historical fiction" },
                        new { ID = 9, GenreType = "Horror" },
                        new { ID = 10, GenreType = "Magical realism" },
                        new { ID = 11, GenreType = "Mystery" },
                        new { ID = 12, GenreType = "Paranoid Fiction" },
                        new { ID = 13, GenreType = "Philosophical" },
                        new { ID = 14, GenreType = "Political" },
                        new { ID = 15, GenreType = "Romance" },
                        new { ID = 16, GenreType = "Saga" },
                        new { ID = 17, GenreType = "Satire" },
                        new { ID = 18, GenreType = "Sci-Fi" },
                        new { ID = 19, GenreType = "Social" },
                        new { ID = 20, GenreType = "Speculative" },
                        new { ID = 21, GenreType = "Thriller" },
                        new { ID = 22, GenreType = "Urban" },
                        new { ID = 23, GenreType = "Western" },
                        new { ID = 24, GenreType = "Animation" },
                        new { ID = 25, GenreType = "Live-action scripted" },
                        new { ID = 26, GenreType = "Live-action unscripted" },
                        new { ID = 27, GenreType = "Crime" }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DirectorID");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("MovieScore");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfVotes");

                    b.HasKey("ID");

                    b.HasIndex("DirectorID");

                    b.ToTable("Movies");

                    b.HasData(
                        new { ID = 1, DirectorID = 1, IsDeleted = false, MovieScore = 9.9, Name = "Venom", NumberOfVotes = 2 },
                        new { ID = 2, DirectorID = 2, IsDeleted = false, MovieScore = 0.0, Name = "A Star Is Born", NumberOfVotes = 0 },
                        new { ID = 3, DirectorID = 3, IsDeleted = false, MovieScore = 0.0, Name = "Hold the Dark", NumberOfVotes = 0 },
                        new { ID = 4, DirectorID = 4, IsDeleted = false, MovieScore = 9.0, Name = "Joker", NumberOfVotes = 1 },
                        new { ID = 5, DirectorID = 5, IsDeleted = false, MovieScore = 8.0, Name = "A Simple Favor", NumberOfVotes = 1 }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.MovieGenre", b =>
                {
                    b.Property<int>("GenreID");

                    b.Property<int>("MovieID");

                    b.HasKey("GenreID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("MovieGenres");

                    b.HasData(
                        new { GenreID = 1, MovieID = 1 },
                        new { GenreID = 18, MovieID = 1 },
                        new { GenreID = 5, MovieID = 2 },
                        new { GenreID = 15, MovieID = 2 },
                        new { GenreID = 2, MovieID = 3 },
                        new { GenreID = 5, MovieID = 3 },
                        new { GenreID = 11, MovieID = 3 },
                        new { GenreID = 21, MovieID = 3 },
                        new { GenreID = 4, MovieID = 4 },
                        new { GenreID = 5, MovieID = 4 },
                        new { GenreID = 3, MovieID = 5 },
                        new { GenreID = 4, MovieID = 5 },
                        new { GenreID = 5, MovieID = 5 },
                        new { GenreID = 11, MovieID = 5 },
                        new { GenreID = 21, MovieID = 5 }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("MovieID");

                    b.Property<double>("MovieRating");

                    b.Property<int>("NumberOfVotes");

                    b.Property<double>("ReviewScore");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");

                    b.HasData(
                        new { ID = 1, IsDeleted = false, MovieID = 1, MovieRating = 10.0, NumberOfVotes = 2, ReviewScore = 8.0, Text = "Mn qko piche", UserID = "1" },
                        new { ID = 2, IsDeleted = false, MovieID = 1, MovieRating = 9.8, NumberOfVotes = 1, ReviewScore = 9.0, Text = "ba i qkiq film, a sym samo user", UserID = "2" },
                        new { ID = 3, IsDeleted = false, MovieID = 5, MovieRating = 8.0, NumberOfVotes = 2, ReviewScore = 0.0, Text = "mn sex", UserID = "1" },
                        new { ID = 4, IsDeleted = false, MovieID = 4, MovieRating = 9.0, NumberOfVotes = 0, ReviewScore = 5.0, Text = "evalata", UserID = "1" }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.ReviewRatings", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReviewId");

                    b.Property<double>("ReviewRating");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("ReviewRatings");

                    b.HasData(
                        new { ID = 1, ReviewId = 1, ReviewRating = 10.0, UserId = "1" },
                        new { ID = 2, ReviewId = 1, ReviewRating = 6.0, UserId = "2" },
                        new { ID = 3, ReviewId = 2, ReviewRating = 9.0, UserId = "1" },
                        new { ID = 4, ReviewId = 3, ReviewRating = 5.0, UserId = "1" },
                        new { ID = 5, ReviewId = 3, ReviewRating = 5.0, UserId = "2" }
                    );
                });

            modelBuilder.Entity("IMDB.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("Rank");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new { Id = "1", AccessFailedCount = 0, ConcurrencyStamp = "d7e696ff-861f-4eb6-a335-9958ccd27f33", EmailConfirmed = false, LockoutEnabled = false, PhoneNumberConfirmed = false, Rank = 2, TwoFactorEnabled = false, UserName = "admin" },
                        new { Id = "2", AccessFailedCount = 0, ConcurrencyStamp = "929d4903-c343-4e61-813f-511d20069299", EmailConfirmed = false, LockoutEnabled = false, PhoneNumberConfirmed = false, Rank = 1, TwoFactorEnabled = false, UserName = "pesho" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("IMDB.Data.Models.Movie", b =>
                {
                    b.HasOne("IMDB.Data.Models.Director", "Director")
                        .WithMany("Movies")
                        .HasForeignKey("DirectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IMDB.Data.Models.MovieGenre", b =>
                {
                    b.HasOne("IMDB.Data.Models.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IMDB.Data.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IMDB.Data.Models.Review", b =>
                {
                    b.HasOne("IMDB.Data.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IMDB.Data.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IMDB.Data.Models.ReviewRatings", b =>
                {
                    b.HasOne("IMDB.Data.Models.Review", "Review")
                        .WithMany("ReviewRatings")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("IMDB.Data.Models.User", "User")
                        .WithMany("ReviewRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("IMDB.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("IMDB.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IMDB.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("IMDB.Data.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}