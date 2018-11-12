using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public sealed class MovieServices : IMovieServices
    {
        private readonly IRepository<Review> reviewRepo;
        private readonly IRepository<MovieGenre> movieGenreRepo;
        private readonly IRepository<Genre> genreRepo;
        private readonly IRepository<Director> directorRepo;
        private readonly IRepository<Movie> movieRepo;

        public MovieServices(
            IRepository<Review> reviewRepo,
            IRepository<Movie> movieRepo,
            IRepository<Director> directorRepo,
            IRepository<Genre> genreRepo,
            IRepository<MovieGenre> movieGenreRepo
            )
        {
            this.reviewRepo = reviewRepo;
            this.movieGenreRepo = movieGenreRepo;
            this.genreRepo = genreRepo;
            this.directorRepo = directorRepo;
            this.movieRepo = movieRepo;
        }
        public async Task<Movie> CreateMovieAsync(string name, ICollection<string> genres, string director)
        {
            Validator.IfNull<ArgumentNullException>(genres);
            Validator.IfNull<ArgumentNullException>(name);
            Validator.IfNull<ArgumentNullException>(director);
            Validator.IfIsNotInRangeInclusive(name.Length, 3, 50, "Movie name cannot be less than 3 and more than 50 letters.");

            Movie movieToAdd = null;
            var foundDirector = await directorRepo.All().FirstOrDefaultAsync(dir => dir.Name.Equals(director));
            if (foundDirector is null)
            {
                Director directorToAdd = new Director() { Name = director };
                movieToAdd = new Movie()
                {
                    Name = name,
                    Director = directorToAdd
                };
            }
            else
            {
                var foundMovie = await movieRepo.All()
                    .Include(mov => mov.Director)
                    .FirstOrDefaultAsync(mov =>
                        mov.Name.ToLower().Equals(name.ToLower())
                        && mov.Director.Name.Equals(director));

                if (foundMovie == null)
                {
                    movieToAdd = new Movie()
                    {
                        Name = name,
                        DirectorID = foundDirector.ID
                    };
                }
                else
                {
                    if (foundMovie.IsDeleted == true)
                    {
                        foundMovie.IsDeleted = false;
                        movieRepo.Update(foundMovie);
                        await movieRepo.SaveAsync();
                        return foundMovie;
                    }
                    else throw new MovieExistsException("Movie already exists!");
                }
            }
            var foundGenres = await genreRepo.All()
                .Include(gr => gr.MovieGenres)
                .Where(genre => genres.Any(genreTypes => genreTypes.ToLower() == genre.GenreType.ToLower()))
                .ToListAsync();
            movieToAdd.MovieGenres = new List<MovieGenre>();
            foreach (var genre in foundGenres)
            {
                var movieGenreToAdd = new MovieGenre
                {
                    GenreID = genre.ID,
                    MovieID = movieToAdd.ID
                };
                movieToAdd.MovieGenres.Add(movieGenreToAdd);
            }
            await movieRepo.AddAsync(movieToAdd);
            await movieRepo.SaveAsync();
            return movieToAdd;
        }
        public async Task<ICollection<Genre>> GetGenresAsync()
        {
            return await genreRepo.All().ToListAsync();
        }
        public async Task<ICollection<Movie>> GetAllMoviesAsync()
        {
            return await movieRepo.All()
                .Include(m => m.Director)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.Reviews)
                    .ThenInclude(r => r.User)
                .ToListAsync();
        }
        public async Task DeleteMovieAsync(int movieID)
        {
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");

            //TODO delete all revies and their stuff
            var movieToDelete = await movieRepo.All()
                                         .Where(mov => mov.ID == movieID && mov.IsDeleted == false)
                                         .Include(m => m.Reviews)
                                         .FirstOrDefaultAsync();
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
            await movieRepo.SaveAsync();
        }
        public async Task<Movie> CheckMovieAsync(int movieID)
        {
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");

            /// <summary>
            /// This piece of code can be optimized! 
            /// </summary>
            var foundMovie = await movieRepo.All()
                .Where(mov => mov.ID == movieID && !mov.IsDeleted)
                .Include(movG => movG.MovieGenres)
                    .ThenInclude(g => g.Genre)
                .Include(movR => movR.Reviews)
                    .ThenInclude(rev => rev.User)
                .Include(movD => movD.Director)
                .SingleOrDefaultAsync();

            if (foundMovie is null)
                throw new MovieNotFoundException("Movie not found!");

            return foundMovie;
        }
        public async Task RateMovieAsync(int movieID, double rating, string reviewText, string curentUserId)
        {
            Validator.IfIsNotPositive(movieID, "MovieID cannot be negative or 0.");
            Validator.IfIsNotInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundMovie = await movieRepo.All()
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(mov => mov.ID == movieID && !mov.IsDeleted);

            if (foundMovie is null)
                throw new MovieNotFoundException("Movie not found!");

            var reviewToAdd = foundMovie.Reviews.FirstOrDefault(rev => rev.MovieID == movieID && rev.UserID == curentUserId);
            if (reviewToAdd != null)
            {
                foundMovie.Reviews.Remove(reviewToAdd);
                foundMovie.NumberOfVotes--;
                if (foundMovie.NumberOfVotes == 0)
                {
                    foundMovie.MovieScore = rating;
                }
                else
                {
                    foundMovie.MovieScore = ((double)(foundMovie.MovieScore * foundMovie.NumberOfVotes) - reviewToAdd.MovieRating) / (double)(foundMovie.NumberOfVotes - 1);
                    foundMovie.MovieScore += (rating - foundMovie.MovieScore) / foundMovie.NumberOfVotes;
                }
                foundMovie.NumberOfVotes++;
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
                    UserID = curentUserId,
                    Text = reviewText
                };
                foundMovie.NumberOfVotes++;
                foundMovie.MovieScore += (rating - foundMovie.MovieScore) / foundMovie.NumberOfVotes;
                foundMovie.Reviews.Add(reviewToAdd);
            }
            movieRepo.Update(foundMovie);
            await movieRepo.SaveAsync();
        }
        public async Task<ICollection<Movie>> SearchMovieAsync(string name, ICollection<string> genres, string director)
        {
            IQueryable<Movie> movies = movieRepo.All()
                .Where(mov => !mov.IsDeleted)
                .Include(mov => mov.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(mov => mov.Director);

            if (name != null)
            {
                movies = movies.Where(mov => mov.Name.Contains(name));
            }

            if (!(genres.First() is null))
            {
                movies = movies
                    .Where(mov => mov.MovieGenres.Any(g => genres.Any(genre => g.Genre.GenreType.ToLower().Contains(genre.ToLower()))));
            }

            if (!string.IsNullOrEmpty(director))
            {
                movies = movies.Where(mov => mov.Director.Name.ToLower().Contains(director.ToLower()));
            }
            var findedMoies = await movies.ToListAsync();
            return findedMoies;
        }
    }
}
