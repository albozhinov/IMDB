using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using IMDB.Services.Exceptions;
using IMDB.Data.Models;
using IMDB.Data.Context;
using IMDB.Services.Providers;
using IMDB.Data.Views;

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
        
        public IEnumerable<ReviewView> ShowReviews(int movieID)
        {
            Validator.IsNonNegative(movieID, "MovieID cannot be negative");

            var foundMovie = this.context.Movies.FirstOrDefault(m => m.ID == movieID);

            if (foundMovie is null || foundMovie.IsDeleted == true)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            
            var reviewsQuery = foundMovie.Reviews
                                    .Where(r => r.IsDeleted == false)
                                    .Select(rev => new ReviewView()
                                    {
                                        Rating = rev.MovieRating,
                                        Score = rev.ReviewScore,
                                        Text = rev.Text,
                                        ByUser = rev.User.UserName,
                                        MovieName = rev.Movie.Name
                                    })
                                    .ToList();

            return reviewsQuery;
        }

        private double CalcualteRating(Review review, double newRating)
        {
            int count = context.ReviewRatings.Count(rev => rev.ReviewId == review.ID);
            double sumAllRatings = context.ReviewRatings.Where(rev => rev.ReviewId == review.ID).Sum(rev => rev.ReviewRating);
            return (sumAllRatings + newRating) / (count + 1);
        }

        public Review RateReview(int reviewID, double score)
        {
            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative");
            Validator.IfIsInRangeInclusive(score, 0D, 10D, "Score is incorrect range.");

            var findReview = this.context.Reviews.FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} not found");
            }

            findReview.ReviewScore = CalcualteRating(findReview, score);
            context.Update(findReview);
            context.SaveChanges();

            return findReview;
        }

        public void DeleteReview(int reviewID)
        {
            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative.");
            

            var findReview = context.Reviews.FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }            
            
            if (findReview.User.ID == loginSession.LoggedUserID || (int)loginSession.LoggedUserRole == adminRank) 
            {
                findReview.IsDeleted = true;               
                context.Reviews.Update(findReview);
            }

            context.SaveChanges();            
        }
    }
}
