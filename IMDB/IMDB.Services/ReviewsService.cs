﻿using IMDB.Data.Contracts;
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
        private int adminRank = 2;

        public ReviewsServices(IRepository<Movie> movie, ILoginSession login, IRepository<Review> review)
        {
            this.movieRepo = movie;
            this.loginSession = login;
            this.reviewRepo = review;
        }
        
        public ICollection<Review> ShowReviews(int movieID)
        {
            Movie findMovie = this.movieRepo.AllButDeleted().FirstOrDefault(m => m.ID == movieID);

            if (findMovie is null)
            {
                throw new ReviewsException("Incorrect movie ID");
            }           

            return findMovie.Reviews;
        }
        
        public void RateReview(int reviewID, int score)
        {
            Review findReview = this.reviewRepo.AllButDeleted().FirstOrDefault(r => r.ID == reviewID);

            if (findReview is null)
            {
                throw new ReviewsException($"Review with ID: {reviewID} not found");
            }

            // Изчакай премоделирането на точките в ревютата, след което довърши командата!

        }

        public void DeleteReview(int reviewID)
        {
            Review findReview = reviewRepo.AllButDeleted().FirstOrDefault(r => r.ID == reviewID);

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
