using IMDB.Data.Contracts;
using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using IMDB.Services.Exceptions;
using IMDB.Data.Models;

namespace IMDB.Services
{
    public class ReviewsServices : IReviewsServices
    {
        private IRepository<Movie> movieRepo;
        private IRepository<Review> reviewRepo;
        private ILoginSession loginSession;

        public ReviewsServices(IRepository<Movie> movie, ILoginSession login)
        {
            this.movieRepo = movie;
            this.loginSession = login;
        }
        
        public ICollection<Review> ShowReviews(int movieID)
        {
            Movie findMovie = this.movieRepo.All().FirstOrDefault(m => m.ID == movieID);

            if (findMovie is null)
            {
                throw new ReviewsException("Incorrect movie ID");
            }           

            return findMovie.Reviews;
        }
        
        public void RateReview(int reviewID, int score)
        {
            Review findReview = this.reviewRepo.All().FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewsException($"Review with ID: {reviewID} not found");
            }

            // Изчакай премоделирането на точките в ревютата, след което довърши командата!

        }

        public void DeleteReview(int reviewID)
        {
            Review findReview = reviewRepo.All().FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewsException($"Review with ID: {reviewID} cannot be deleted. ID is invalid.");
            }

            // Обсъди с Генерала дали може loginSession.LoggedUser 
            //да е от тип User. ПРоверката да е по userID, а не по name!!!
            if (findReview.User.UserName == loginSession.LoggedUser || loginSession.LoggedUserRole.ToLower() == "admin") 
            {
                reviewRepo.Delete(findReview);
            }

            // Review трябва да има Property bool isDeleted !!!
            reviewRepo.Save();
        }
    }
}
