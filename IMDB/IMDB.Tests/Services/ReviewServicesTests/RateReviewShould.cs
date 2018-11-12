using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Tests.Services.ReviewServicesTests
{
    [TestClass]
    public class RateReviewShould
    {
        [DataTestMethod]
        [DataRow(0, 9.00)]
        [DataRow(null, 9.00)]
        [DataRow(2, -1.00)]
        [DataRow(2, 11.00)]
        public async Task ThrowArgumentException_WhenParametersAreIncorrect(int reviewId, double rating)
        {
            // Arrange            
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

            var reviewMock = new Review()
            {
                ID = 1,
                IsDeleted = false,
                Text = "Text",
            };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await reviewServices.RateReviewAsync(reviewId, rating, "randomUserId"));
        }

        [DataTestMethod]
        [DataRow(5, false)]
        [DataRow(1, true)]
        public async Task ThrowReviewNotFoundException_WhenReviewIDIsIncorrect(int reviewId, bool flag)
        {
            // Arrange            
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

            var reviewMock = new Review()
            {
                ID = 1,
                IsDeleted = flag,
                Text = "Text",
            };

            var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ReviewNotFoundException>(async () => await reviewServices.RateReviewAsync(reviewId, 9D, "randomUserId"));
        }

        [TestMethod]
        public async Task UpdateReviewScoreUpdatingReview_WhenParametersAreCorrect()
        {
            // Arrange    
            const int reviewID = 1;
            const double rating = 5;
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

            var user = new User() { Id = "1", UserName = "Gosho" };
            var movie = new Movie() { Name = "Mecho Puh" };
            var reviewRating = new ReviewRatings() { ID = 1, ReviewId = 1, UserId = "1", ReviewRating = 6 }; //thats gonna change to 5
            var reviewRating2 = new ReviewRatings() { ID = 2, ReviewId = 1, UserId = "2", ReviewRating = 4 };

            var reviewMock = new Review() { ID = 1, IsDeleted = false, Text = "Text", Movie = movie, User = user, UserID = user.Id, MovieID = movie.ID, ReviewRatings = new List<ReviewRatings>() { reviewRating, reviewRating2 }, NumberOfVotes = 2, MovieRating = 9.95, ReviewScore = 5 }; //The review score is 5 = (6 + 4)/ 2

            var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act
            var result = await reviewServices.RateReviewAsync(reviewID, rating, "1");

            // Assert
            reviewRepoMock.Verify(revRepo => revRepo.SaveAsync(), Times.Once);
            reviewRepoMock.Verify(revRepo => revRepo.Update(reviewMock), Times.Once);
            Assert.AreSame(result, reviewMock);
            Assert.IsTrue(reviewMock.ReviewScore == 4.5); //the rating for userID 1 has changed from 6 to 5, hence now the score should be (4 + 5) / 2 = 4.5
        }        
    }
}
