using IMDB.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.IO;

namespace IMDB.Data.Context
{
	public class IMDBContext : IdentityDbContext<User>
	{
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<MovieGenre> MovieGenres { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<ReviewRatings> ReviewRatings { get; set; }
		public DbSet<Director> Directors { get; set; }


		public IMDBContext(DbContextOptions<IMDBContext> options)
			: base(options)
		{
		}

		private void LoadJsonInDB(ModelBuilder modelBuilder)
		{
			try
			{
				var genresAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\genres.json");
				var moviesAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\movies.json");
				var movieGenresAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\movieGenres.json");
				var usersAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\users.json");
				var reviewsAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\reviews.json");
				var reviewRatingsAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\reviewratings.json");
				var directorsAsJson = File.ReadAllText(@"..\IMDB.Data\JsonGoodness\directors.json");

				var genres = JsonConvert.DeserializeObject<Genre[]>(genresAsJson);
				var movies = JsonConvert.DeserializeObject<Movie[]>(moviesAsJson);
				var movieGenres = JsonConvert.DeserializeObject<MovieGenre[]>(movieGenresAsJson);
				var users = JsonConvert.DeserializeObject<User[]>(usersAsJson);
				var reviews = JsonConvert.DeserializeObject<Review[]>(reviewsAsJson);
				var ratingReviews = JsonConvert.DeserializeObject<ReviewRatings[]>(reviewRatingsAsJson);
				var directors = JsonConvert.DeserializeObject<Director[]>(directorsAsJson);

				modelBuilder.Entity<Genre>().HasData(genres);
				modelBuilder.Entity<Movie>().HasData(movies);
				modelBuilder.Entity<MovieGenre>().HasData(movieGenres);
				modelBuilder.Entity<User>().HasData(users);
				modelBuilder.Entity<Review>().HasData(reviews);
				modelBuilder.Entity<ReviewRatings>().HasData(ratingReviews);
				modelBuilder.Entity<Director>().HasData(directors);
			}
			catch (Exception)
			{
				return;
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			LoadJsonInDB(modelBuilder);

			modelBuilder.Entity<Movie>().Property(mov => mov.Name)
				.IsRequired()
				.HasMaxLength(50);
			modelBuilder.Entity<Movie>().Property(mov => mov.DirectorID)
				.IsRequired();

			modelBuilder.Entity<ReviewRatings>()
				.HasOne(rr => rr.User)
				.WithMany(u => u.ReviewRatings)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<ReviewRatings>()
				.HasOne(rr => rr.Review)
				.WithMany(r => r.ReviewRatings)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<MovieGenre>()
				.HasKey(m => new { m.GenreID, m.MovieID });
			base.OnModelCreating(modelBuilder);
		}

	}
}
