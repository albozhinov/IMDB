//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using IMDB.Data.Models;
//using IMDB.Data.Repository;
//using IMDB.Services;
//using IMDB.Services.Contracts;
//using IMDB.Services.Exceptions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace IMDB.Tests.Services.ReviewServicesTests
//{
//    [TestClass]
//    public class DeleteReviewShould
//    {
//        [DataTestMethod]
//        [DataRow(null)]
//        [DataRow(0)]
//        public void ThrowArgumentException_WhenParametersAreIncorrect(int reviewId)
//        {
//            // Arrange
//            var movieRepoStub = new Mock<IRepository<Movie>>();
//            var reviewRepoMock = new Mock<IRepository<Review>>();
//            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

//            var reviewMock = new Review()
//            {
//                ID = 1,
//                IsDeleted = false,
//                Text = "Mnogo qk Unit Test!"
//            };

//            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
//            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

//            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

//            // Act and Assert
//            Assert.ThrowsException<ArgumentException>(() => reviewServices.DeleteReview(reviewId, "dawda", "somerole"));
//        }

//        [DataTestMethod]
//        [DataRow(null, "not null")]
//        [DataRow("not null", null)]
//        public void ThrowArgumentNullException_WhenParementersAreNull(string id, string role)
//        {
//            // Arrange
//            var movieRepoStub = new Mock<IRepository<Movie>>();
//            var reviewRepoMock = new Mock<IRepository<Review>>();
//            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

//            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act and Assert
        //    Assert.ThrowsException<ArgumentNullException>(() => reviewServices.DeleteReview(12, id, role));
        //}

//        [DataTestMethod]
//        [DataRow(1, true)]
//        [DataRow(5, false)]
//        public void ThrowReviewNotFoundException_WhenReviewNotFound(int reviewId, bool flag)
//        {
//            // Arrange
//            var movieRepoStub = new Mock<IRepository<Movie>>();
//            var reviewRepoMock = new Mock<IRepository<Review>>();
//            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

//            var reviewMock = new Review()
//            {
//                ID = 1,
//                IsDeleted = flag,
//                Text = "Mnogo qk Unit Test!"
//            };

//            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
//            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

//            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

//            // Act and Assert
//            Assert.ThrowsException<ReviewNotFoundException>(() => reviewServices.DeleteReview(reviewId, "someID", "someRole"));
//        }

//        [DataTestMethod]
//        [DataRow("5", "Administrator")]
//        [DataRow("10", "User")]
//        public void DeletedReview_WhenUserHasPermission(string userID, string role)
//        {
//            // Arrange
//            const int reviewId = 1;
//            const bool deletedFlag = false;
//            var movieRepoStub = new Mock<IRepository<Movie>>();
//            var reviewRepoMock = new Mock<IRepository<Review>>();
//            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

//            var user = new User()
//            {
//                Id = "10",
//                UserName = "Stamat"
//            };

//            var movie = new Movie()
//            {
//                Name = "American Pie",
//                ID = 1,
//                NumberOfVotes = 100,
//            };

//            var reviewMock = new Review()
//            {
//                ID = 1,
//                IsDeleted = deletedFlag,
//                Text = "Mnogo qk Unit Test!",
//                UserID = user.Id,
//                User = user,
//                Movie = movie
//            };

//            var allReviews = new List<Review>() { reviewMock }.AsQueryable();
//            reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

//            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);
//            // Act
//            reviewServices.DeleteReview(reviewId, userID, role);

//            // Assert
//            Assert.IsTrue(reviewMock.IsDeleted == true);
//            Assert.IsTrue(reviewMock.Movie.NumberOfVotes == 99);
//            reviewRepoMock.Verify(rRepo => rRepo.Update(reviewMock), Times.Once);
//            reviewRepoMock.Verify(rRepo => rRepo.Save(), Times.Once);
//        }
//    }
//}
