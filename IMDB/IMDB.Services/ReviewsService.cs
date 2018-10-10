using IMDB.Data.Contracts;
using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IMDB.Services.Exceptions;
using IMDB.Data.Models;

namespace IMDB.Services
{
    public class ReviewsService : IReviewsService
    {
        private IMovieRepo movie;

        public ReviewsService(IMovieRepo movie)
        {
            this.movie = movie;
        }
        
        public ICollection<Review> ShowReviews(int movieID)
        {
            if (this.movie.GetMovie(movieID) is null)
            {
                throw new ReviewsException("Incorrect movie ID");
            }

            var reviews = this.movie.GetReviews(movieID);

            return reviews;
        }
    }
}
