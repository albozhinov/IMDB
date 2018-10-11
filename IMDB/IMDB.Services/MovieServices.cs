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
			//TODO add permissions for all services if the user is authorizied
		}
		public void CreateMovie(string name, string genre, string producer)
		{
			//Validate name, genre and producer for format
			//if genre doesnt exists add it.
			var findIfExists = movieRepo.All()
				.FirstOrDefault(mov => mov.Producer.ToLower() == producer.ToLower() && mov.Name.ToLower() == name.ToLower());
				
			if(findIfExists == null)
			{
				var movieToAdd = new Movie()
				{
					Name = name,
					//Genre = genre,
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
			//Validate movie ID
			var movieToDelete = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (movieToDelete is null)
				throw new MovieNotFoundException();
			movieRepo.Delete(movieToDelete);
			movieRepo.Save();
		}

		public Movie Check(int movieID)
		{
			//Validate movie ID
			var foundMovie = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException();
			return foundMovie;
		}
		public void RateMovie(int movieID, double rating, string reviewText)
		{
			//Validate movie ID, rating and review text
			var foundMovie = this.movieRepo.All().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException();
			var reviewToAdd = new Review()
			{
				MovieID = movieID,
				MovieRating = rating,
				UserID = loginSession.LoggedUserID,
				Text = reviewText
			};
			foundMovie.Reviews.Add(reviewToAdd);
			movieRepo.Update(foundMovie);
			movieRepo.Save();
			//TODO update the rating on the movie SHABAN its you here!
		}
	}
}
