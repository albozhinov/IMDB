using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using IMDB.Services.Exceptions;
using IMDB.Data.Models;
using IMDB.Data.Context;

namespace IMDB.Services
{
    public class ReviewsServices : IReviewsServices
    {
        private IMDBContext context;
        private ILoginSession loginSession;
        private const int adminRank = 2;

        public ReviewsServices(IMDBContext context, ILoginSession login)
        {
            this.context = context;
            this.loginSession = login;            
        }
        
        public IEnumerable<Review> ShowReviews(int movieID)
        {
            if (!this.context.Movies.Any(m => m.ID == movieID))
            {
                throw new MovieNotFoundException("Movie not found.");
            }


            var reviewsQuery = this.context.Reviews.Where(r => r.MovieID == movieID);                    

            return reviewsQuery.ToList();
        }
        
        public Review RateReview(int reviewID, int score)
        {
            Review findReview = this.reviewRepo.AllButDeleted().FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewsException($"Review with ID: {reviewID} not found");
            }

            // Изчакай премоделирането на точките в ревютата, след което довърши командата!

        }

        public Review DeleteReview(int reviewID)
        {
            var findReview = context.Reviews.FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewsException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }            
            
            if (findReview.User.ID == loginSession.LoggedUserID || loginSession.LoggedUserRank == adminRank) 
            {
                reviewRepo.Delete(findReview);                
            }

            reviewRepo.Save();
        }
    }
}
