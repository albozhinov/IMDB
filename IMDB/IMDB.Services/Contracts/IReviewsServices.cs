using IMDB.Data.Models;
using IMDB.Data.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Contracts
{
    public interface IReviewsServices
    {
		/// <summary>
		/// Lists all reviews for a movie
		/// </summary>
		/// <param name="movieId"></param>
		/// <returns></returns>
		Task<ICollection<Review>> ListMovieReviewsAsync(int movieID);
		/// <summary>
		/// Rates review, updadtes the score, and returns it to be seen (updadtes it)
		/// </summary>
		/// <param name="reviewID"></param>
		/// <param name="socre"></param>
		/// <returns></returns>
		Task<Review> RateReviewAsync(int reviewID, double rating, string currentUserId);
		/// <summary>
		/// Deltes a review (just a flag)
		/// </summary>
		/// <param name="reviewID"></param>
		Task<Review> DeleteReviewAsync(int reviewID);

	}
}
