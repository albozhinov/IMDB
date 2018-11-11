using IMDB.Data.Models;
using IMDB.Data.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Contracts
{
	public interface IMovieServices
	{
		/// <summary>
		/// Creates a movie if one with the same name and producer doesnt exist
		/// </summary>
		/// <param name="name"></param>
		/// <param name="genre"></param>
		/// <param name="producer"></param>
		Task<Movie> CreateMovieAsync(string name, ICollection<string> genre, string producer);
		/// <summary>
		/// Deletes movie via its ID
		/// </summary>
		/// <param name="movieID"></param>
		Task DeleteMovieAsync(int movieID);
        /// <summary>
        /// Rates a movie or updates its rating for the logged user
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="rating"></param>
        /// <param name="reviewText"></param>
        Task RateMovieAsync(int movieID, double rating, string reviewText, string curentUserId);
        /// <summary>
        /// Returns the movie with all of its info, including the top 5 rating reviews, returning the movie's name
        /// </summary>
        /// <param name="movieID"></param>
        /// <returns></returns>
		Task<Movie> CheckMovieAsync(int movieID);
        /// <summary>
        /// Searches for movie via name and/or genre or/and producer. Specify with null if a parameter is not included.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="genre"></param>
        /// <param name="producer"></param>
        /// <returns></returns>
        Task<ICollection<Movie>> SearchMovieAsync(string name, ICollection<string> genres, string producer);
		/// <summary>
		/// Returns all movies.
		/// </summary>
		/// <returns></returns>
		Task<ICollection<Movie>> GetAllMoviesAsync();
		/// <summary>
		/// Returns all movie genres
		/// </summary>
		/// <returns></returns>
		Task<ICollection<Genre>> GetGenresAsync();
	}
}
