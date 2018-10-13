using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
    public class ReviewsService : IReviewsServices
    {
        private IRepository<ReviewRatings> reviewRatingsRepo;
        private IRepository<Movie> movieRepo;
        private IRepository<Review> reviewRepo;
        private ILoginSession loginSession;
        private const int adminRank = 2;

        public ReviewsService(
            IRepository<Review> reviewRepo, 
            IRepository<Movie> movieRepo,
            IRepository<ReviewRatings> reviewRatingsRepo,
            ILoginSession login)
        {
            this.reviewRatingsRepo = reviewRatingsRepo;
            this.movieRepo = movieRepo;
            this.reviewRepo = reviewRepo;
            loginSession = login;
        }

        public IEnumerable<ReviewView> ListMovieReviews(int movieID)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permision bro.");

            Validator.IsNonNegative(movieID, "MovieID cannot be negative.");

            var foundMovie = movieRepo.All().FirstOrDefault(m => m.ID == movieID);

            if (foundMovie is null || foundMovie.IsDeleted == true)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            var reviewsQuery = reviewRepo.All()
                                    .Where(r => r.MovieID == movieID && r.IsDeleted == false)
                                    .Select(rev => new ReviewView()
                                    {
                                        ReviewID = rev.ID,
                                        Rating = rev.MovieRating,
                                        Score = rev.ReviewScore,
                                        Text = rev.Text,
                                        ByUser = rev.User.UserName,
                                        MovieName = rev.Movie.Name
                                    })
                                    .OrderByDescending(rev => rev.Score)
                                    .ToList();

            return reviewsQuery;
        }

        private double CalcualteRating(Review review, double newRating)
        {
            int count = reviewRatingsRepo.All().Count(rev => rev.ReviewId == review.ID);
            double sumAllRatings = reviewRatingsRepo.All().Where(rev => rev.ReviewId == review.ID).Sum(rev => rev.ReviewRating);
            return (sumAllRatings + newRating) / (count + 1);
        }

        public ReviewView RateReview(int reviewID, double rating)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permision bro.");

            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative.");
            Validator.IfIsInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundReview = reviewRepo.All()
                                         .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                         .Include(r => r.User)
                                         .Include(r => r.Movie)
                                         .Include(r => r.ReviewRatings)
                                         .ToList()
                                         .FirstOrDefault();

            if (foundReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} not found.");
            }

            foundReview.ReviewScore = CalcualteRating(foundReview, rating);

            var reviewRatingToAdd = new ReviewRatings()
            {
                ReviewId = foundReview.ID,
                UserId = loginSession.LoggedUserID,
                ReviewRating = rating,
            };

            foundReview.ReviewRatings.Add(reviewRatingToAdd);
            reviewRepo.Update(foundReview);            
            reviewRepo.Save();

            var revView = new ReviewView()
            {
                ReviewID = foundReview.ID,
                Rating = foundReview.MovieRating,
                Score = foundReview.ReviewScore,
                ByUser = foundReview.User.UserName,
                MovieName = foundReview.Movie.Name,
                Text = foundReview.Text
            };

            return revView;
        }

        public void DeleteReview(int reviewID)
        {
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("Not enough permision bro.");

            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative.");

            var findReview = reviewRepo.All()
                                       .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                       .Include(r => r.User)
                                       .ToList()
                                       .FirstOrDefault();

            if (findReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }


            if (findReview.User.ID == loginSession.LoggedUserID || (int)loginSession.LoggedUserRole == adminRank)
            {
                findReview.IsDeleted = true;
                reviewRepo.Update(findReview);
            }

            reviewRepo.Save();
        }
    }
}
