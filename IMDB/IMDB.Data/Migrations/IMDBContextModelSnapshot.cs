﻿// <auto-generated />
using IMDB.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMDB.Data.Migrations
{
	[DbContext(typeof(IMDBContext))]
	partial class IMDBContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
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

			modelBuilder.Entity("IMDB.Data.Models.Permissions", b =>
				{
					b.Property<int>("ID")
						.ValueGeneratedOnAdd()
						.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

					b.Property<int>("Rank");

					b.Property<string>("Text")
						.IsRequired();

					b.HasKey("ID");

					b.ToTable("Permissions");

					b.HasData(
						new { ID = 1, Rank = 0, Text = "register" },
						new { ID = 2, Rank = 0, Text = "login" },
						new { ID = 3, Rank = 0, Text = "checkmovie" },
						new { ID = 4, Rank = 0, Text = "searchmovie" },
						new { ID = 5, Rank = 0, Text = "listmoviereviews" },
						new { ID = 6, Rank = 1, Text = "ratemovie" },
						new { ID = 7, Rank = 1, Text = "ratereview" },
						new { ID = 8, Rank = 1, Text = "deletereview" },
						new { ID = 9, Rank = 1, Text = "logout" },
						new { ID = 10, Rank = 2, Text = "deletemovie" },
						new { ID = 11, Rank = 2, Text = "createmovie" },
						new { ID = 12, Rank = 2, Text = "listmoviestopdf" }

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

					b.Property<int>("UserID");

					b.HasKey("ID");

					b.HasIndex("MovieID");

					b.HasIndex("UserID");

					b.ToTable("Reviews");

					b.HasData(
						new { ID = 1, IsDeleted = false, MovieID = 1, MovieRating = 10.0, NumberOfVotes = 2, ReviewScore = 8.0, Text = "Mn qko piche", UserID = 1 },
						new { ID = 2, IsDeleted = false, MovieID = 1, MovieRating = 9.8, NumberOfVotes = 1, ReviewScore = 9.0, Text = "ba i qkiq film, a sym samo user", UserID = 2 },
						new { ID = 3, IsDeleted = false, MovieID = 5, MovieRating = 8.0, NumberOfVotes = 2, ReviewScore = 0.0, Text = "mn sex", UserID = 1 },
						new { ID = 4, IsDeleted = false, MovieID = 4, MovieRating = 9.0, NumberOfVotes = 0, ReviewScore = 5.0, Text = "evalata", UserID = 1 }
					);
				});

			modelBuilder.Entity("IMDB.Data.Models.ReviewRatings", b =>
				{
					b.Property<int>("ID")
						.ValueGeneratedOnAdd()
						.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

					b.Property<int>("ReviewId");

					b.Property<double>("ReviewRating");

					b.Property<int>("UserId");

					b.HasKey("ID");

					b.HasIndex("ReviewId");

					b.HasIndex("UserId");

					b.ToTable("ReviewRatings");

					b.HasData(
						new { ID = 1, ReviewId = 1, ReviewRating = 10.0, UserId = 1 },
						new { ID = 2, ReviewId = 1, ReviewRating = 6.0, UserId = 2 },
						new { ID = 3, ReviewId = 2, ReviewRating = 9.0, UserId = 1 },
						new { ID = 4, ReviewId = 3, ReviewRating = 5.0, UserId = 1 },
						new { ID = 5, ReviewId = 3, ReviewRating = 5.0, UserId = 2 }
					);
				});

			modelBuilder.Entity("IMDB.Data.Models.User", b =>
				{
					b.Property<int>("ID")
						.ValueGeneratedOnAdd()
						.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

					b.Property<string>("Password")
						.IsRequired();

					b.Property<int>("Rank");

					b.Property<string>("UserName")
						.IsRequired()
						.HasMaxLength(50);

					b.HasKey("ID");

					b.ToTable("Users");

					b.HasData(
						new { ID = 1, Password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918", Rank = 2, UserName = "admin" },
						new { ID = 2, Password = "325E7C598DE838FD1438C818A75E703FDDF7BA91C922EB68F2E0239705DFFA79", Rank = 1, UserName = "pesho" }
					);
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
#pragma warning restore 612, 618
		}
	}
}
