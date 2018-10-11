﻿using IMDB.Data.Models;
using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
    interface IReviewsServices
    {
        IEnumerable<Review> ShowReviews(int movieId);

        Review RateReview(int reviewID, double socre);

        void DeleteReview(int reviewID);
    }
}
