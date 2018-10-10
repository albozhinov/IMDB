using IMDB.Data.Models;
using System.Collections.Generic;

namespace IMDB.Data.Contracts
{
	public interface IMovieRepo
	{
		void AddMovie(string name, string genre, string producer);
		Movie GetMovie(string name, string producer);
		Movie GetMovie(int movieID);
		void UnDeleteMovie(int movieID);
		void DeleteMovie(int movieID);
        ICollection<Review> GetReviews(int movieID);
	}
}
