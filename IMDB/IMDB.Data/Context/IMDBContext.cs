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
					.UseSqlServer(@"Server=DESKTOP-4T206OM;Database=IMBD;Trusted_Connection=True;");
            }
        }
		private void LoadJsonInDB(ModelBuilder modelBuilder)
		{
			var genresAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\genres.json");
			var permissionsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\permissions.json");
			var moviesAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\movies.json");
			var movieGenresAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\movieGenres.json");
			var usersAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\users.json");
			var reviewsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\reviews.json");
			var reviewRatingsAsJson = File.ReadAllText(@"..\IMDB\JsonGoodness\reviewratings.json");
			
			var genres = JsonConvert.DeserializeObject<Genre[]>(genresAsJson);
			var permissions = JsonConvert.DeserializeObject<Permissions[]>(permissionsAsJson);
			var movies = JsonConvert.DeserializeObject<Movie[]>(moviesAsJson);
			var movieGenres = JsonConvert.DeserializeObject<MovieGenre[]>(movieGenresAsJson);
			var users = JsonConvert.DeserializeObject<User[]>(usersAsJson);
			var reviews = JsonConvert.DeserializeObject<Review[]>(reviewsAsJson);
			var ratingReviews = JsonConvert.DeserializeObject<ReviewRatings[]>(reviewRatingsAsJson);

			modelBuilder.Entity<Genre>().HasData(genres);
			modelBuilder.Entity<Permissions>().HasData(permissions);
			modelBuilder.Entity<Movie>().HasData(movies);
			modelBuilder.Entity<MovieGenre>().HasData(movieGenres);
			modelBuilder.Entity<User>().HasData(users);
			modelBuilder.Entity<Review>().HasData(reviews);
			modelBuilder.Entity<ReviewRatings>().HasData(ratingReviews);
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().Property(mov => mov.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>().Property(us => us.UserName)
                .HasMaxLength(50)
                .IsRequired();

			//LoadJsonInDB(modelBuilder);

			modelBuilder.Entity<MovieGenre>()
				.HasKey(m => new { m.GenreID, m.MovieID });
			base.OnModelCreating(modelBuilder);
        }

    }
}
