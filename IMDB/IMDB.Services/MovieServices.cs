using IMDB.Data.Context;
using IMDB.Data.Contracts;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
	public sealed class MovieServices : IMovieServices
	{
		private IRepository<Movie> movieRepo;
		private ILoginSession loginSession;
		private IRepository<Genre> genreRepo;
		private IRepository<MovieGenre> movieGenreRepo;

		public MovieServices(IRepository<Movie> movieRepo, IRepository<Genre> genreRepo, IRepository<MovieGenre> movieGenreRepo,ILoginSession loginSession)
		{
			this.movieRepo = movieRepo;
			this.loginSession = loginSession;
			this.genreRepo = genreRepo;
			this.movieGenreRepo = movieGenreRepo;
			//TODO add permissions for all services if the user is authorizied
		}
		public void CreateMovie(string name, ICollection<string> genres, string producer)
		{
			//Validate name, genre and producer for format
			var findIfExists = movieRepo.All()
				.FirstOrDefault(mov => mov.Producer.ToLower() == producer.ToLower() && mov.Name.ToLower() == name.ToLower());

			Movie movieToAdd = null;
			if (findIfExists == null)
			{
				movieToAdd = new Movie()
				{
					Name = name,
					Producer = producer
				};
				this.movieRepo.Add(movieToAdd);
			}
			else
			{
				if (findIfExists.IsDeleted == false)
				{
					//TODO restore all deleted posts and their stuff
					findIfExists.IsDeleted = true;
					movieRepo.Update(findIfExists);
					movieRepo.Save();
					return;
				}
				else
					throw new MovieExistsException();
			}
			movieRepo.Save();

			var foundGenres = this.genreRepo.All().Where(gO => genres.Any(gS => gS == gO.GenreType));
			foreach(var genre in foundGenres)
			{
				var movieGenreToAdd = new MovieGenre
				{
					GenreID = genre.ID,
					MovieID = movieToAdd.ID
				};
				movieGenreRepo.Add(movieGenreToAdd);
			}
			movieGenreRepo.Save();
		}

		public void DeleteMovie(int movieID)
		{
			//Validate movie ID
			//TODO delete all revies and their stuff
			var movieToDelete = this.movieRepo.AllButDeleted().FirstOrDefault(mov => mov.ID == movieID);
			if (movieToDelete is null)
				throw new MovieNotFoundException("Movie not found!");
			movieRepo.Delete(movieToDelete);
			movieRepo.Save();
		}

		public Movie Check(int movieID)
		{
			//Validate movie ID
			var foundMovie = this.movieRepo.AllButDeleted().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");
			return foundMovie;
		}
		public void RateMovie(int movieID, double rating, string reviewText)
		{
			//Validate movie ID, rating and review text
			var foundMovie = this.movieRepo.AllButDeleted().FirstOrDefault(mov => mov.ID == movieID);
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");
			//TODO see if exists and enable it
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
