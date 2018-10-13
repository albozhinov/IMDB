using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
    public sealed class MovieServices : IMovieServices
	{
        private IRepository<Review> reviewRepo;
        private IRepository<MovieGenre> movieGenreRepo;
        private IRepository<Genre> genreRepo;
        private IRepository<Director> directorRepo;
        private IRepository<Movie> movieRepo;
        private ILoginSession loginSession;
        private IReviewsServices reviewServices;
        private const int adminRank = 2;

		public MovieServices(
            IRepository<Review> reviewRepo,
            IRepository<Movie> movieRepo, 
            IRepository<Director> directorRepo, 
            IRepository<Genre> genreRepo, 
            IRepository<MovieGenre> movieGenreRepo, 
            ILoginSession loginSession, IReviewsServices reviewServices)
		{
            this.reviewRepo = reviewRepo;
            this.movieGenreRepo = movieGenreRepo;
            this.genreRepo = genreRepo;
            this.directorRepo = directorRepo;
            this.movieRepo = movieRepo;
			this.loginSession = loginSession;
            this.reviewServices = reviewServices; //Maybe a better way?

			//TODO add permissions for all services if the user is authorizied
		}

        private bool CheckProducerExists(string directorName)
        {
            var findProducer = this.directorRepo.All().FirstOrDefault(prod => prod.Name.Equals(directorName));
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
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            //Validate name, genre and director for format - Done?
            Validator.IfIsInRangeInclusive(name.Length, 3, 50, "Movie name cannot be less than 3 and more than 50 letters.");
            Validator.IfNull<ArgumentNullException>(genres);

            Movie movieToAdd = null;
            if (!CheckProducerExists(director))
            {
                Director producerToAdd = new Director() { Name = director };
                movieToAdd = new Movie()
                {
                    Name = name,
                    DirectorID = producerToAdd.ID
                };
                movieRepo.Add(movieToAdd);
                movieRepo.Save();
            }
            else
            {
                var foundMovie = movieRepo.All() 
					.Include(mov => mov.Director)
					.FirstOrDefault(mov => 
						mov.Name.ToLower().Equals(name.ToLower())
						&& mov.Director.Name.Equals(director));

                if (foundMovie == null)
                {
                    movieToAdd = new Movie()
                    {
                        Name = name,
                        DirectorID = directorRepo.All().FirstOrDefault(prod => prod.Name.Equals(director)).ID
                    };
                    movieRepo.Add(movieToAdd);
                    movieRepo.Save();
                }
                else
                {
                    if (foundMovie.IsDeleted == true)
                    {
                        foundMovie.IsDeleted = false;
                        movieRepo.Update(foundMovie);
                        movieRepo.Save();
                        return;
                    }
                    else throw new MovieExistsException("Movie already exists!");
                }

                var foundGenres = genreRepo.All().Where(gO => genres.Any(gS => gS == gO.GenreType)).ToList();
                foreach (var genre in foundGenres)
                {
                    var movieGenreToAdd = new MovieGenre
                    {
                        GenreID = genre.ID,
                        MovieID = movieToAdd.ID
                    };
                    movieGenreRepo.Add(movieGenreToAdd);
                    movieGenreRepo.Save();
                }
            }
        }

		public void DeleteMovie(int movieID)
		{
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");

			//TODO delete all revies and their stuff
			var movieToDelete = movieRepo.All()
                                         .Where(mov => mov.ID == movieID && mov.IsDeleted == false)
                                         .ToList()
                                         .FirstOrDefault();
            if (movieToDelete is null)
            {
                throw new MovieNotFoundException("Movie not found!");
            }
            else if ((int)loginSession.LoggedUserRole == adminRank)
            {
                movieToDelete.IsDeleted = true;
                var reviews = reviewRepo.All()
                                        .Where(rev => rev.MovieID == movieToDelete.ID && rev.IsDeleted == false)
                                        .ToList();

                foreach (var review in reviews)
                {
                    review.IsDeleted = true;
					reviewRepo.Update(review);
                }
                movieRepo.Update(movieToDelete);
                movieRepo.Save();
                reviewRepo.Save();
            }
            else
            {
                throw new NotEnoughPermissionException("Not enough permission bro");
            }
        }
		//Checking when such movie exists - works
		//checking when such movie doesnt exist - works
		public MovieView Check(int movieID)
		{            
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");

			var foundMovie = movieRepo.All()
				.Where(mov => mov.ID == movieID && !mov.IsDeleted)
				.Select(mov => new MovieView
				{
					Name = mov.Name,
					Genres = mov.MovieGenres.Select(movG => movG.Genre.GenreType).ToList(),
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
		public void RateMovie(int movieID, double rating, string reviewText)
		{
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");
            Validator.IfIsInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundMovie = movieRepo.All().FirstOrDefault(mov => mov.ID == movieID && !mov.IsDeleted);
			if (foundMovie is null)
				throw new MovieNotFoundException("Movie not found!");

            var reviewToAdd = reviewRepo.All().FirstOrDefault(rev => rev.MovieID == movieID && rev.UserID == loginSession.LoggedUserID);
            if (reviewToAdd != null)
            {
                foundMovie.MovieScore = CalcualteUpdateRating(foundMovie, rating, reviewToAdd.MovieRating);

                reviewToAdd.IsDeleted = false;
                reviewToAdd.Text = reviewText;
                reviewToAdd.MovieRating = rating;
                reviewRepo.Update(reviewToAdd);
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
                foundMovie.MovieScore = CalcualteNewRating(foundMovie, rating);
                reviewRepo.Add(reviewToAdd);
            }
            
			movieRepo.Update(foundMovie);
            movieRepo.Save();
		}

        private double CalcualteNewRating(Movie movie , double newRating)
        {
            int count = reviewRepo.All().Count(rev => rev.MovieID == movie.ID);
            double sumAllRatings = reviewRepo.All().Where(rev => rev.MovieID == movie.ID && !rev.IsDeleted).Sum(rev => rev.MovieRating);
            return (sumAllRatings + newRating) / (count + 1);
        }
        private double CalcualteUpdateRating(Movie movie, double newRating, double oldRating)
        {
            int count = reviewRepo.All().Count(rev => rev.MovieID == movie.ID);
            double sumAllRatings = reviewRepo.All().Where(rev => rev.MovieID == movie.ID && !rev.IsDeleted).Sum(rev => rev.MovieRating) - oldRating;
            return (sumAllRatings + newRating) / count;
        }

        public ICollection<Movie> SearchMovies(string name, string genre, string producer)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            IQueryable<Movie> movies;
            if (name!=null)
            {
                 movies = movieRepo.All().Where(mov => mov.Name.Contains(name) && mov.IsDeleted==false);
            }
            else
            {
                movies = movieRepo.All().Where(mov=>mov.IsDeleted==false);
            }
            if (genre != null)
            {

                movies = movies
                    .Where(mov => mov.MovieGenres.Any(mg => mg.Genre.GenreType == genre));
            }
            if (producer != null)
            {
                movies = movies.Include(mov => mov.Director).Where(mov => mov.Director.Name.Equals(producer));
            }
            if (movies.ToList() != null)
            {
                return movies.ToList();
            }
			throw new MovieNotFoundException("Movie not found!");

        }
    }
}
