using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ICollection<Review>> ListMovieReviewsAsync(int movieID)
        {
            Validator.IfIsNotPositive(movieID, "Movie ID cannot be negative or 0.");
            var foundMovie = await movieRepo.All()
									.FirstOrDefaultAsync(m => m.ID == movieID && m.IsDeleted == false);

            if (foundMovie is null || foundMovie.IsDeleted == true)
            {
                throw new MovieNotFoundException("Movie not found.");
            }
            var reviewsQuery = await reviewRepo.All()
                                    .Where(r => r.MovieID == movieID && r.IsDeleted == false)
                                    .Include(revUser => revUser.User)
                                    .ToListAsync();
            return reviewsQuery;
        }

        public async Task<Review> RateReviewAsync(int reviewID, double rating, string currentUserId)
        {
            Validator.IfIsNotPositive(reviewID, "ReviewID cannot be negative or 0.");
            Validator.IfIsNotInRangeInclusive(rating, 0D, 10D, "Score is in incorrect range.");

            var foundReview = await reviewRepo.All()
                                        .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                        .Include(r => r.User)
                                        .Include(r => r.Movie)
                                        .Include(r => r.ReviewRatings)
                                        .FirstOrDefaultAsync();

            if (foundReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} not found.");
            }           

            // Check logic!!!
            var reviewRatingToUpdate = foundReview.ReviewRatings
                                                  .FirstOrDefault(r => r.UserId == currentUserId
                                                                    && r.ReviewId == foundReview.ID);

            if (reviewRatingToUpdate is null)
            {
                var reviewRatingToAdd = new ReviewRatings()
                {
                    ReviewId = foundReview.ID,
                    UserId = currentUserId,
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
                    if (foundReview.NumberOfVotes == 0)
                    {
                        foundReview.ReviewScore = 0;
                    }
                    else
                    {
                        foundReview.ReviewScore = ((foundReview.ReviewScore * foundReview.NumberOfVotes) - reviewRatingToUpdate.ReviewRating) / (foundReview.NumberOfVotes - 1);
                    }
                    
                    foundReview.ReviewScore += (rating - foundReview.ReviewScore) / foundReview.NumberOfVotes;
                }
                reviewRatingToUpdate.ReviewRating = rating;
            }

            reviewRepo.Update(foundReview);
            await reviewRepo.SaveAsync();

            return foundReview;
        }

        public async Task<Review> DeleteReviewAsync(int reviewID)
        {
            Validator.IfIsNotPositive(reviewID, "ReviewID cannot be negative or 0.");

            var foundReview = await reviewRepo.All()
                                       .Where(rev => rev.ID == reviewID && rev.IsDeleted == false)
                                       .Include(r => r.User)
                                       .Include(r => r.Movie)
                                       .FirstOrDefaultAsync();

            if (foundReview is null)
            {
                throw new ReviewNotFoundException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }

            foundReview.IsDeleted = true;
            foundReview.Movie.NumberOfVotes--;

            // TODO: Modified this function
            if (foundReview.Movie.NumberOfVotes == 0)
            {
                foundReview.Movie.MovieScore = 0;
            }
            else if (foundReview.Movie.NumberOfVotes == 1)
            {
                foundReview.Movie.MovieScore = (foundReview.Movie.MovieScore * foundReview.Movie.NumberOfVotes) - foundReview.MovieRating;                
            }            
            else
            {
                foundReview.Movie.MovieScore = ((foundReview.Movie.MovieScore * foundReview.Movie.NumberOfVotes) - foundReview.MovieRating) / (foundReview.Movie.NumberOfVotes - 1);
            }
            reviewRepo.Update(foundReview);

            await reviewRepo.SaveAsync();
            return foundReview;
        }
    }
}
