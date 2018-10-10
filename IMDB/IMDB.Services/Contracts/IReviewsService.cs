using IMDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Services.Contracts
{
    interface IReviewsServices
    {
        ICollection<Review> ShowReviews(int movieId);

        void RateReview(int reviewID, int socre);

        void DeleteReview(int reviewID);
    }
}
