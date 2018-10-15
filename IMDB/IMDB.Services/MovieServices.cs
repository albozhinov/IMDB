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
        private const int adminRank = 2;

        public MovieServices(
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

            //TODO add permissions for all services if the user is authorizied
        }
        public void CreateMovie(string name, ICollection<string> genres, string director)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            Validator.IfNull<ArgumentNullException>(genres);
            Validator.IfNull<ArgumentNullException>(name);
            Validator.IfNull<ArgumentNullException>(director);
            Validator.IfIsNotInRangeInclusive(name.Length, 3, 50, "Movie name cannot be less than 3 and more than 50 letters.");

            Movie movieToAdd = null;
            var foundDirector = directorRepo.All().FirstOrDefault(dir => dir.Name.Equals(director));
            if (foundDirector is null)
            {
                Director directorToAdd = new Director() { Name = director };
                movieToAdd = new Movie()
                {
                    Name = name,
                    DirectorID = directorToAdd.ID
                };
                directorRepo.Add(directorToAdd);
                directorRepo.Save();
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
                        DirectorID = foundDirector.ID
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
            }
            var foundGenres = genreRepo.All()
                .Include(gr => gr.MovieGenres)
                .Where(genre => genres.Any(genreTypes => genreTypes == genre.GenreType));
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

        public void DeleteMovie(int movieID)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");

            //TODO delete all revies and their stuff
            var movieToDelete = movieRepo.All()
                                         .Where(mov => mov.ID == movieID && mov.IsDeleted == false)
                                         .Include(m => m.Reviews)
                                         .FirstOrDefault();
            if (movieToDelete is null)
            {
                throw new MovieNotFoundException("Movie not found!");
            }

            movieToDelete.IsDeleted = true;

            foreach (var review in movieToDelete.Reviews)
            {
                review.IsDeleted = true;
            }
            movieRepo.Update(movieToDelete);
            movieRepo.Save();
        }
        public MovieView Check(int movieID)
        {
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");

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
                        Text = rev.Text,
						NumberOfVotes = rev.NumberOfVotes
                    })
                        .ToList(),
                    Score = mov.MovieScore,
                    Director = mov.Director.Name,
					NumberOfVotes = mov.NumberOfVotes
                })
                .FirstOrDefault();
            if (foundMovie is null)
                throw new MovieNotFoundException("Movie not found!");
            return foundMovie;
        }
        public void RateMovie(int movieID, double rating, string reviewText)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");
            Validator.IfIsNotInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundMovie = movieRepo.All().Include(m => m.Reviews).FirstOrDefault(mov => mov.ID == movieID && !mov.IsDeleted);
            if (foundMovie is null)
                throw new MovieNotFoundException("Movie not found!");

            var reviewToAdd = foundMovie.Reviews.FirstOrDefault(rev => rev.MovieID == movieID && rev.UserID == loginSession.LoggedUserID);
            if (reviewToAdd != null)
            {
				foundMovie.Reviews.Remove(reviewToAdd);
                foundMovie.MovieScore = ((double)(foundMovie.MovieScore * foundMovie.NumberOfVotes) - reviewToAdd.MovieRating) / (double)(foundMovie.NumberOfVotes - 1);
				foundMovie.MovieScore += (rating - foundMovie.MovieScore) / foundMovie.NumberOfVotes;
				reviewToAdd.IsDeleted = false;
                reviewToAdd.Text = reviewText;
                reviewToAdd.MovieRating = rating;
				foundMovie.Reviews.Add(reviewToAdd);
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
				foundMovie.NumberOfVotes++;
				foundMovie.MovieScore += (rating - foundMovie.MovieScore) / foundMovie.NumberOfVotes;
				foundMovie.Reviews.Add(reviewToAdd);
            }
            movieRepo.Update(foundMovie);
            movieRepo.Save();
        }
        public ICollection<MovieView> SearchMovie(string name, string genre, string director)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permission bro");
            IQueryable<MovieView> movies = movieRepo.All().Where(mov=>!mov.IsDeleted).Select(mov => new MovieView
            {
                Name = mov.Name,
                Genres = mov.MovieGenres.Select(movG => movG.Genre.GenreType).ToList(),
                Top5Reviews = new List<ReviewView>(),
                Score = mov.MovieScore,
                Director = mov.Director.Name,
                NumberOfVotes = mov.NumberOfVotes
            });

            if (name != null)
            {
                movies = movies.Where(mov => mov.Name.Contains(name));
            }
            if (genre != null)
            {
                movies = movies
                    .Where(mov => mov.Genres.Select(g => g.ToLower()).Contains(genre));
            }
            if (director != null)
            {
                movies = movies.Where(mov => mov.Director.ToLower() == director.ToLower());
            }
            var findedMoies = movies.ToList();
            if (findedMoies.Count == 0)
            {
                throw new MovieNotFoundException("Movie not found!");
            }
            return findedMoies;

        }
    }
}
