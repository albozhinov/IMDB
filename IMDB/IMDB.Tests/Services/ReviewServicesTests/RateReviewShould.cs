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
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var reviewStub = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();
            loginStub.Setup(l => l.LoggedUserPermissions).Returns(new List<string>() { "Not", "Enough", "Permission" });

            var movieMock = new Movie()
            {
                ID = 1,
                Name = "Rambo",
                IsDeleted = true
            };

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object, loginStub.Object);

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
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var reviewStub = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();
            loginStub.Setup(l => l.LoggedUserPermissions).Returns(new List<string>() { "cmd0", "cmd1", "ratereview" });

            var movieMock = new Movie()
            {
                ID = 1,
                Name = "Rambo",
                IsDeleted = true
            };

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => reviewServices.RateReview(reviewId, rating));            
        }

        [DataTestMethod]
        [DataRow(5, false)]
        [DataRow(1, true)]
        public void ThrowReviewNotFoundException_WhenReviewIDIsIncorrect(int reviewId, bool isDeleted)
        {
            // Arrange            
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var reviewStub = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();
            loginStub.Setup(l => l.LoggedUserPermissions).Returns(new List<string>() { "cmd0", "cmd1", "ratereview" });

            var movieMock = new Movie()
            {
                ID = 1,
                Name = "Rambo",
                IsDeleted = isDeleted
            };

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object, loginStub.Object);

            // Act and Assert
            Assert.ThrowsException<ReviewNotFoundException>(() => reviewServices.RateReview(reviewId, 9D));
        }
    }
}
