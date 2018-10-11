using IMDB.Data.Models;
using System.Collections.Generic;

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
		void CreateMovie(string name, ICollection<string> genre, string producer);
		/// <summary>
		/// Deletes movie via its ID
		/// </summary>
		/// <param name="movieID"></param>
		void DeleteMovie(int movieID);
		/// <summary>
		/// Rates a movie or updates its rating for the logged user
		/// </summary>
		/// <param name="movieID"></param>
		/// <param name="rating"></param>
		/// <param name="reviewText"></param>
		void RateMovie(int movieID, double rating, string reviewText);
		Movie Check(int movieID);
	}
}
