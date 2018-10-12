using IMDB.Data.Models;
using IMDB.Data.Views;
using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
    public interface IReviewsServices
    {
        IEnumerable<ReviewView> ListMovieReviews(int movieId);

        ReviewView RateReview(int reviewID, double socre);

        void DeleteReview(int reviewID);
    }
}
