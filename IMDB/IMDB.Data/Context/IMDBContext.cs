using IMDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace IMDB.Data.Context
{
    public class IMDBContext : DbContext
    {
		private static readonly LoggerFactory loggerFactory;

		static IMDBContext()
		{
			var loggerProviders = new List<ILoggerProvider>()
			{
				new ConsoleLoggerProvider(
						(category, level) =>
							category == DbLoggerCategory.Database.Command.Name &&
							level == LogLevel.Information,
						includeScopes: true
				)
			};

			loggerFactory = new LoggerFactory(loggerProviders);
		}


		public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Permissions> Permitions { get; set; }
		public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReviewRatings> ReviewRatings { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
					.UseLoggerFactory(loggerFactory)
					.UseSqlServer(@"Server=DESKTOP-UQ2T66F\MSSQLSERVER01;Database=IMBD;Trusted_Connection=True;");
            }
        }
		private void LoadJsonInDB(ModelBuilder modelBuilder)
		{
			var genresAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\genres.txt");
			var permissionsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\permissions.txt");
			var moviesAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\movies.txt");
			var movieGenresAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\movieGenres.txt");
			var usersAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\users.txt");
			var reviewsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\reviews.txt");
			var reviewRatingsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\reviewratings.txt");
			
			var genres = JsonConvert.DeserializeObject<Genre[]>(genresAsJson);
			var permissions = JsonConvert.DeserializeObject<Permissions[]>(permissionsAsJson);
			var movies = JsonConvert.DeserializeObject<Genre[]>(moviesAsJson);
			var movieGenres = JsonConvert.DeserializeObject<Genre[]>(movieGenresAsJson);
			var users = JsonConvert.DeserializeObject<Genre[]>(usersAsJson);
			var reviews = JsonConvert.DeserializeObject<Genre[]>(reviewsAsJson);
			var ratingReviews = JsonConvert.DeserializeObject<Genre[]>(reviewRatingsAsJson);

			modelBuilder.Entity<Genre>().HasData(genres);

		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			LoadJsonInDB(modelBuilder);

			modelBuilder.Entity<MovieGenre>()
				.HasKey(m => new { m.GenreID, m.MovieID });
			base.OnModelCreating(modelBuilder);
        }

    }
}
