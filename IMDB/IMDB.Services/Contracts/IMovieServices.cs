using IMDB.Data.Models;

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
		void CreateMovie(string name, string genre, string producer);
		/// <summary>
		/// Deletes movie via its ID
		/// </summary>
		/// <param name="movieID"></param>
		void DeleteMovie(int movieID);
		void RateMovie(int movieID, double rating, string reviewText);
		Movie Check(int movieID);
	}
}
