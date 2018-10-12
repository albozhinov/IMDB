using IMDB.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.Collections.Generic;

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
        public DbSet<Permition> Permitions { get; set; }
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<MovieGenre>()
				.HasKey(m => new { m.GenreID, m.MovieID });
			base.OnModelCreating(modelBuilder);
        }

    }
}
