using IMDB.Data.Models;
using IMDB.Data.Views;
using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
    public interface IReviewsServices
    {
        /// <summary>
        /// Lists all reviews for a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        ICollection<Review> ListMovieReviews(int movieId);
        /// <summary>
        /// Rates review, updadtes the score, and returns it to be seen (updadtes it)
        /// </summary>
        /// <param name="reviewID"></param>
        /// <param name="socre"></param>
        /// <returns></returns>
        Review RateReview(int reviewID, double rating, string currentUserID);
        /// <summary>
        /// Deltes a review (just a flag)
        /// </summary>
        /// <param name="reviewID"></param>
        void DeleteReview(int reviewID, string curentUserId, string curentUserRole);
    }
}
