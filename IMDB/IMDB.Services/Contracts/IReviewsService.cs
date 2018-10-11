using IMDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Services.Contracts
{
    interface IReviewsServices
    {
        IEnumerable<Review> ShowReviews(int movieId);

        Review RateReview(int reviewID, int socre);

        Review DeleteReview(int reviewID);
    }
}
