using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDB.Tests.Services.ReviewServicesTests
{
    [TestClass]
    public class RateReviewShould
    {
        [TestMethod]
        public void ThrowNotEnoughPermissionsException_WhenUserHasNotLoggedIn()
        {
            // Arrange
            int movieId = 1;
            double rating = 5.00;
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();

            var reviewMock = new Review()
            {
                ID = 1,
                IsDeleted = false,
                Text = "Text",
            };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act and Assert
            Assert.ThrowsException<NotEnoughPermissionException>(() => reviewServices.RateReview(movieId, rating));
        }

        [DataTestMethod]
        [DataRow(0, 9.00)]
        [DataRow(null, 9.00)]
        [DataRow(2, -1.00)]
        [DataRow(2, 11.00)]
        public void ThrowArgumentException_WhenParametersAreIncorrect(int reviewId, double rating)
        {
            // Arrange            
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();

            var reviewMock = new Review()
            {
                ID = 1,
                IsDeleted = false,
                Text = "Text",
            };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => reviewServices.RateReview(reviewId, rating));
        }

        [DataTestMethod]
        [DataRow(5, false)]
        [DataRow(1, true)]
        public void ThrowReviewNotFoundException_WhenReviewIDIsIncorrect(int reviewId, bool flag)
        {
            // Arrange            
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();

            var reviewMock = new Review()
            {
                ID = 1,
                IsDeleted = flag,
                Text = "Text",
            };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act and Assert
            Assert.ThrowsException<ReviewNotFoundException>(() => reviewServices.RateReview(reviewId, 9D));
        }

        [TestMethod]
        public void UpdateReviewScoreUpdatingReview_WhenParametersAreCorrect()
        {
            // Arrange    
            const int reviewID = 1;
            const double rating = 8.88;
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();

            var user = new User() { Id = "1", UserName = "Gosho" };
            var movie = new Movie() { Name = "Mecho Puh" };
            var reviewRating = new ReviewRatings() { ID = 1, ReviewId = 1, UserId = "1", ReviewRating = 5 };

            var reviewMock = new Review() { ID = 1, IsDeleted = false, Text = "Text", Movie = movie, User = user, UserID = user.Id, MovieID = movie.ID, ReviewRatings = new List<ReviewRatings>() { reviewRating }, NumberOfVotes = 10, MovieRating = 9.95, ReviewScore = 7.77 };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act
            var result = reviewServices.RateReview(reviewID, rating);

            // Assert
            reviewRepoMock.Verify(revRepo => revRepo.Save(), Times.Once);
            reviewRepoMock.Verify(revRepo => revRepo.Update(reviewMock), Times.Once);
            Assert.IsTrue(result.ByUser == reviewMock.User.UserName);
            Assert.IsTrue(result.MovieName == reviewMock.Movie.Name);
            Assert.IsTrue(result.NumberOfVotes == reviewMock.NumberOfVotes);
            Assert.IsTrue(result.Rating == reviewMock.MovieRating);
            Assert.IsTrue(result.ReviewID == reviewMock.ID);            
            Assert.IsTrue(result.Text == reviewMock.Text);
        }        
    }
}
