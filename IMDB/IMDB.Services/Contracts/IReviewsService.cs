using IMDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Services.Contracts
{
    interface IReviewsService
    {
        ICollection<Review> ShowReviews(int movieId);
    }
}
