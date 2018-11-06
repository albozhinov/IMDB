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
        private const string adminRole = "Administrator";

        public ReviewsService(
            IRepository<Review> reviewRepo, 
            IRepository<Movie> movieRepo,
            IRepository<ReviewRatings> reviewRatingsRepo)
        {
            this.reviewRatingsRepo = reviewRatingsRepo;
            this.movieRepo = movieRepo;
            this.reviewRepo = reviewRepo;
        }

        public ICollection<Review> ListMovieReviews(int movieID)
        {
            Validator.IfIsNotPositive(movieID, "Movie ID cannot be negative or 0.");
            var foundMovie = movieRepo.All().FirstOrDefault(m => m.ID == movieID && m.IsDeleted == false);

            if (foundMovie is null || foundMovie.IsDeleted == true)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            var reviewsQuery = reviewRepo.All()
                                    .Where(r => r.MovieID == movieID && r.IsDeleted == false)
                                    .Include(revUser => revUser.User)
                                    .ToList();            
            return reviewsQuery;
        }

        public Review RateReview(int reviewID, double rating, string currentUserID)
        {
            Validator.IfIsNotPositive(reviewID, "ReviewID cannot be negative or 0.");
            Validator.IfIsNotInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundReview = reviewRepo.All()
                                        .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                        .Include(r => r.User)
                                        .Include(r => r.Movie)
                                        .Include(r => r.ReviewRatings)
                                        .FirstOrDefault();

            if (foundReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} not found.");
            }
            // Check logic!!!
            var reviewRatingToUpdate = foundReview.ReviewRatings
                                                  .FirstOrDefault(r => r.UserId == currentUserID
                                                                    && r.ReviewId == foundReview.ID);

            if(reviewRatingToUpdate is null)
            {
                var reviewRatingToAdd = new ReviewRatings()
                {
                    ReviewId = foundReview.ID,
                    UserId = currentUserID,
                    ReviewRating = rating,
                };

				foundReview.NumberOfVotes++;
				foundReview.ReviewScore += (rating - foundReview.ReviewScore) / foundReview.NumberOfVotes;
                foundReview.ReviewRatings.Add(reviewRatingToAdd);
			}
            else
            {
                if (foundReview.NumberOfVotes == 1)
                {
                    foundReview.ReviewScore = rating;
                }
                else
                {
                    foundReview.ReviewScore = ((foundReview.ReviewScore * foundReview.NumberOfVotes) - reviewRatingToUpdate.ReviewRating) / (foundReview.NumberOfVotes - 1);
                    foundReview.ReviewScore += (rating - foundReview.ReviewScore) / foundReview.NumberOfVotes;
                }
                reviewRatingToUpdate.ReviewRating = rating;
			}

            reviewRepo.Update(foundReview);
            reviewRepo.Save();

            return foundReview;
        }

        public void DeleteReview(int reviewID, string curentUserId, string curentUserRole)
        {
            Validator.IfIsNotPositive(reviewID, "ReviewID cannot be negative or 0.");
            Validator.IfNull<ArgumentNullException>(curentUserRole, "Current user role cannot be null!");
            Validator.IfNull<ArgumentNullException>(curentUserId, "Current user id cannot be null!");

            var findReview = reviewRepo.All()
                                       .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                       .Include(r => r.User)
									   .Include(r => r.Movie)
                                       .FirstOrDefault();

            if (findReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }

            if (findReview.User.Id == curentUserId || curentUserRole == adminRole)
            {
                findReview.IsDeleted = true;
                findReview.Movie.MovieScore = ((findReview.Movie.MovieScore * findReview.Movie.NumberOfVotes) - findReview.MovieRating) / (findReview.Movie.NumberOfVotes - 1);
                findReview.Movie.NumberOfVotes--;
                reviewRepo.Update(findReview);
            }
            else
            {
                throw new NotEnoughPermissionException("Not enough permission bro.");
            }

            reviewRepo.Save();
        }
    }
}
