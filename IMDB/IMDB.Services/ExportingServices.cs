using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using SautinSoft.Document;
using System.Linq;

namespace IMDB.Services
{
	public sealed class ExportingServices : IExportingServices
	{

		private IRepository<Movie> movieRepo;

		public ExportingServices(
			IRepository<Movie> movieRepo)
		{
			this.movieRepo = movieRepo;
		}
		public void ListMoviesToPDF()
		{
			var movies = movieRepo.All()
				.Select(mov => new MovieView
				{
                    ID = mov.ID,
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
                .OrderByDescending(m => m.Score)
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

