using IMDB.Data.Contracts;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using System.Linq;

namespace IMDB.Services
{
	public sealed class MovieServices : IMovieServices
	{
		private IRepository<Movie> movieRepo;
		private ILoginSession loginSession;

		public MovieServices(IRepository<Movie> movieRepo, ILoginSession loginSession)
		{
			this.movieRepo = movieRepo;
			this.loginSession = loginSession;
			//TODO add permissions
		}
		public void CreateMovie(string name, string genre, string producer)
		{
			var findIfExists = movieRepo.All()
				.FirstOrDefault(mov => mov.Producer.ToLower() == producer.ToLower() && mov.Name.ToLower() == name.ToLower());
				
			if(findIfExists == null)
			{
				var movieToAdd = new Movie()
				{
					Name = name,
					Genre = genre,
					Producer = producer
				};
				this.movieRepo.Add(movieToAdd);
			}
			else
			{
				if (findIfExists.IsDeleted == false)
				{
					findIfExists.IsDeleted = true;
					movieRepo.Update(findIfExists);
				}
				else
					throw new MovieExistsException();
			}
			movieRepo.Save();
		}

		public void DeleteMovie(int movieID)
		{
			var movieToDelete = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (movieToDelete is null)
				throw new MovieNotFoundException();
			movieRepo.Delete(movieToDelete);
			movieRepo.Save();
		}

		public Movie Check(int movieID)
		{
			var foundMovie = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException();
			return foundMovie;
		}
		public void RateMovie(int movieID, double rating, string reviewText)
		{
			var foundMovie = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException();
			var reviewToAdd = new Review()
			{
				MovieID = movieID,
				//Score = rating,
				Text = reviewText
			};
			foundMovie.Reviews.Add(reviewToAdd);
			movieRepo.Update(foundMovie);
			movieRepo.Save();
			//TODO update the rating on the movie
		}
	}
}
