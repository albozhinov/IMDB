using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using SautinSoft.Document;
using System.Linq;
using System.Text;

namespace IMDB.Services
{
	public sealed class ExportingServices : IExportingServices
	{

		private IRepository<Review> reviewRepo;
		private IRepository<MovieGenre> movieGenreRepo;
		private IRepository<Genre> genreRepo;
		private IRepository<Director> directorRepo;
		private IRepository<Movie> movieRepo;
		private ILoginSession loginSession;
		private const int adminRank = 2;

		public ExportingServices(
			IRepository<Review> reviewRepo,
			IRepository<Movie> movieRepo,
			IRepository<Director> directorRepo,
			IRepository<Genre> genreRepo,
			IRepository<MovieGenre> movieGenreRepo,
			ILoginSession loginSession)
		{
			this.reviewRepo = reviewRepo;
			this.movieGenreRepo = movieGenreRepo;
			this.genreRepo = genreRepo;
			this.directorRepo = directorRepo;
			this.movieRepo = movieRepo;
			this.loginSession = loginSession;
		}
		public void ListMoviesToPDF()
		{
			if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
				throw new NotEnoughPermissionException("Not enough permission bro");

			var movies = movieRepo.All()
				.OrderByDescending(m => m.MovieScore)
				.Select(mov => new MovieView
				{
					Name = mov.Name,
					Genres = mov.MovieGenres.Select(movG => movG.Genre.GenreType).ToList(),
					Top5Reviews = mov.Reviews.OrderByDescending(rev => rev.ReviewScore).Select(rev => new ReviewView
					{
						ReviewID = rev.ID,
						ByUser = rev.User.UserName,
						Score = rev.ReviewScore,
						MovieName = rev.Movie.Name,
						Rating = rev.MovieRating,
						Text = rev.Text,
						NumberOfVotes = rev.NumberOfVotes
					})
						.ToList(),
					Score = mov.MovieScore,
					Director = mov.Director.Name,
					NumberOfVotes = mov.NumberOfVotes
				})
				.ToList();

			DocumentCore dc = new DocumentCore();
			dc.Content.End.Insert("Movie list: " + '\n', new CharacterFormat() { FontName = "Verdana", Size = 35.5f, FontColor = Color.Orange });
			foreach (var movie in movies)
			{
				dc.Content.End.Insert(new string('#', 40) + '\n', new CharacterFormat() { FontName = "Verdana", Size = 7.5f, FontColor = Color.Orange });
				dc.Content.End.Insert(movie.ToString(), new CharacterFormat() { FontName = "Verdana", Size = 7.5f });
			}
			// Save a document to a file in PDF format.
			string filePath = @"Result.pdf";
			dc.Save(filePath);

			// Open the result for demonstation purposes.
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
		}
	}
}

