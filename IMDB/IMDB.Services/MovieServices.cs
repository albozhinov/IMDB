using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
    public sealed class MovieServices : IMovieServices
	{
		private ILoginSession loginSession;
		private IMDBContext context;

		public MovieServices(IMDBContext context, ILoginSession loginSession)
		{
			this.context = context;
			this.loginSession = loginSession;
			//TODO add permissions for all services if the user is authorizied
		}

		public void CreateMovie(string name, ICollection<string> genres, string producer)
		{
            //Validate name, genre and producer for format - Done?
            var foundMovie = context.Movies.FirstOrDefault(mov => mov.Name.ToLower().Equals(name.ToLower()) && mov.Producer.ToLower().Equals(producer.ToLower()));

			Movie movieToAdd = null;
			if (foundMovie == null)
			{
				movieToAdd = new Movie()
				{
					Name = name,
					Producer = producer
				};
				this.context.Add(movieToAdd);
			}
			else
			{
				if (foundMovie.IsDeleted == true)
				{
					//TODO restore all deleted posts and their stuff - no need of that
					foundMovie.IsDeleted = false;
					context.Movies.Update(foundMovie);
                    context.SaveChanges();
					return;
				}else throw new MovieExistsException();
			}

			var foundGenres = this.context.Genres.Where(gO => genres.Any(gS => gS == gO.GenreType));
			foreach(var genre in foundGenres)
			{
				var movieGenreToAdd = new MovieGenre
				{
					GenreID = genre.ID,
					MovieID = movieToAdd.ID
				};
				context.Add(movieGenreToAdd);
			}
			context.SaveChanges();
		}

		public void DeleteMovie(int movieID)
		{
			//Validate movie ID
			//TODO delete all revies and their stuff
			var movieToDelete = this.context.Movies.FirstOrDefault(mov => mov.ID == movieID);
            if (movieToDelete is null)
                throw new MovieNotFoundException("Movie not found!");
            else {
                movieToDelete.IsDeleted = true;
                var reviews = context.Reviews.Where(rev => rev.MovieID == movieToDelete.ID);
                foreach (var review in reviews)
                {
                    review.IsDeleted = true;
					context.Reviews.Update(review);
                }
            }
			context.SaveChanges();
		}

		public Movie Check(int movieID)
		{
			//Validate movie ID
			var foundMovie = this.context.Movies.Where(mov => mov.ID == movieID).First();
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");
			return foundMovie;
		}

		public void RateMovie(int movieID, double rating, string reviewText)
		{
			//Validate movie ID, rating and review text
			var foundMovie = this.context.Movies.FirstOrDefault(mov => mov.ID == movieID);
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
            context.Reviews.Add(reviewToAdd);
            //context.Movies.

            //TODO update the rating on the movie SHABAN its you here! 
            //Shaban: DONE!
            foundMovie.MovieScore = CalcualteRating(foundMovie, rating);
			context.Movies.Update(foundMovie);
            context.SaveChanges();

		}

        private double CalcualteRating(Movie movie , double newRating)
        {
            int count = context.Reviews.Count(rev => rev.MovieID == movie.ID);
            double sumAllRatings = context.Reviews.Where(rev => rev.MovieID == movie.ID).Sum(rev => rev.MovieRating);
            return (sumAllRatings + newRating) / (count + 1);
        }

        public ICollection<Movie> SearchMovies(string name, string genre, string producer)
        {
            IQueryable<Movie> movies;
            if (name!=null)
            {
                 movies = context.Movies.Where(mov => mov.Name.Contains(name));
            }
            else
            {
                movies = context.Set<Movie>();
            }

            if (genre!=null)
            {
                var query = (from movies in context.Movies
                             join 
                    );
            }

        }
    }
}
