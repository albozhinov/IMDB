using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using Microsoft.EntityFrameworkCore;
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

        private bool CheckProducerExists(string directorName)
        {
            var findProducer = context.Directors.FirstOrDefault(prod => prod.Name.Equals(directorName));
            if (findProducer != null)
            {
                return true;
            }
            else return false;
        }
		//Creating movie when such exists - works
		//creating movie when such exists, but it is deleted - works
		//creating movie when one doesnt exist - works
        public void CreateMovie(string name, ICollection<string> genres, string director)
        {
            //Validate name, genre and producer for format - Done?
            Validator.IfIsInRangeInclusive(name.Length, 3, 50, "Movie name cannot be less than 3 and more than 50 letters.");


            Movie movieToAdd = null;
            if (!CheckProducerExists(director))
            {
                Director producerToAdd = new Director() { Name = director };
                movieToAdd = new Movie()
                {
                    Name = name,
                    DirectorID = producerToAdd.ID
                };
                this.context.Movies.Add(movieToAdd);
            }
            else
            {
                var foundMovie = context.Movies
					.Include(mov => mov.Director)
					.FirstOrDefault(mov => 
						mov.Name.ToLower().Equals(name.ToLower())
						&& mov.Director.Name.Equals(director));

                if (foundMovie == null)
                {
                    movieToAdd = new Movie()
                    {
                        Name = name,
                        DirectorID = context.Directors.FirstOrDefault(prod => prod.Name.Equals(director)).ID
                    };
                    this.context.Movies.Add(movieToAdd);
                }
                else
                {
                    if (foundMovie.IsDeleted == true)
                    {
                        foundMovie.IsDeleted = false;
                        context.Movies.Update(foundMovie);
                        context.SaveChanges();
                        return;
                    }
                    else throw new MovieExistsException("Movie already exists!");
                }

                var foundGenres = this.context.Genres.Where(gO => genres.Any(gS => gS == gO.GenreType));
                foreach (var genre in foundGenres)
                {
                    var movieGenreToAdd = new MovieGenre
                    {
                        GenreID = genre.ID,
                        MovieID = movieToAdd.ID
                    };
                    context.MovieGenres.Add(movieGenreToAdd);
                }
            }
            context.SaveChanges();
        }

		public void DeleteMovie(int movieID)
		{            
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");

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
		//Checking when such movie exists - works
		//checking when such movie doesnt exist - works
		public MovieView Check(int movieID)
		{            
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");

			var foundMovie = this.context.Movies
				.Where(mov => mov.ID == movieID && !mov.IsDeleted)
				.Select(mov => new MovieView
				{
					Name = mov.Name,
					Genres = mov.MovieGenres.Select(movG => movG.Genre.GenreType),
					Top5Reviews = mov.Reviews.OrderByDescending(rev => rev.ReviewScore).Take(5).Select(rev => new ReviewView
						{
                            ReviewID = rev.ID,
							ByUser = rev.User.UserName,
							Score = rev.ReviewScore,
							MovieName = rev.Movie.Name,
							Rating = rev.MovieRating,
							Text = rev.Text
						})
						.ToList(),
					Score = mov.MovieScore,
					Director = mov.Director.Name
				})
				.FirstOrDefault();
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");
			return foundMovie;
		}

        //updates a rating
        //creates a new one successfully
        //successfully updates the rating on the movie
		public int RateMovie(int movieID, double rating, string reviewText)
		{
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");
            Validator.IfIsInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundMovie = this.context.Movies.FirstOrDefault(mov => mov.ID == movieID && mov.IsDeleted==false);
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");

            var reviewToAdd = this.context.Reviews.FirstOrDefault(rev => rev.MovieID == movieID && rev.UserID == loginSession.LoggedUserID);
            if (reviewToAdd != null)
            {
                reviewToAdd.IsDeleted = false;
                reviewToAdd.Text = reviewText;
                reviewToAdd.MovieRating = rating;
                context.Reviews.Update(reviewToAdd);
            }
            else
            {
                reviewToAdd = new Review()
                {
                    MovieID = movieID,
                    MovieRating = rating,
                    UserID = loginSession.LoggedUserID,
                    Text = reviewText
                };
                context.Reviews.Add(reviewToAdd);
            }
            
            //TODO update the rating on the movie SHABAN its you here! 
            //Shaban: DONE!
            foundMovie.MovieScore = CalcualteRating(foundMovie, rating);
			context.Movies.Update(foundMovie);
            context.SaveChanges();

            return reviewToAdd.ID;
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
                 movies = context.Movies.Where(mov => mov.Name.Contains(name) && mov.IsDeleted==false);
            }
            else
            {
                movies = context.Set<Movie>().Where(mov=>mov.IsDeleted==false);
            }
            if (genre != null)
            {

                movies = context.Movies
                    .Where(mov => mov.MovieGenres.Any(mg => mg.Genre.GenreType == genre));
            }
            if (producer != null)
            {
                movies = context.Movies.Include(mov => mov.Director).Where(mov => mov.Director.Name.Equals(producer));
            }
            if (movies.ToList() != null)
            {
                return movies.ToList();
            }
			throw new MovieNotFoundException("Movie not found!");

        }
    }
}
