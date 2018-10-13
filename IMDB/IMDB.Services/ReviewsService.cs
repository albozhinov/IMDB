using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
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
            Validator.IsNonNegative(movieID, "MovieID cannot be negative");

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
            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative");
            Validator.IfIsInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var findReview = reviewRepo.All()
                                         .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                         .Select(r => new Review()
                                         {
                                             ID = r.ID,
                                             IsDeleted = r.IsDeleted,
                                             MovieID = r.MovieID,
                                             MovieRating = r.MovieRating,
                                             ReviewScore = r.ReviewScore,
                                             Text = r.Text,
                                             UserID = r.UserID,
                                             Movie = r.Movie,
                                             User = r.User,
                                             ReviewRatings = r.ReviewRatings
                                         }).FirstOrDefault();

            if (findReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} not found");
            }

            findReview.ReviewScore = CalcualteRating(findReview, rating);
            reviewRepo.Update(findReview);

            reviewRepo.Save();

            var revView = new ReviewView()
            {
                ReviewID = findReview.ID,
                Rating = findReview.MovieRating,
                Score = findReview.ReviewScore,
                ByUser = findReview.User.UserName,
                MovieName = findReview.Movie.Name,
                Text = findReview.Text
            };

            return revView;
        }

        public void DeleteReview(int reviewID)
        {
            Validator.IsNonNegative(reviewID, "ReviewID cannot be negative.");

            var findReview = reviewRepo.All().FirstOrDefault(r => r.ID == reviewID);

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
