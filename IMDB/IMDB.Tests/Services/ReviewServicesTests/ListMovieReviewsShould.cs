using IMDB.Data.Context;
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
    public class ListMovieReviewsShould
    {
        [DataTestMethod]
        [DataRow(5, false)]
        [DataRow(1, true)]
        public void ThrowMovieNotFoundException_WhenMovieNotFound(int movieId, bool isDeleted)
        {
            // Arrange
            var movieRepoMock = new Mock<IRepository<Movie>>();           
            var reviewStub = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();
            loginStub.Setup(l => l.LoggedUserPermissions).Returns(new List<string>() { "cmd0", "cmd1", "listmoviereviews" });

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
            Assert.ThrowsException<MovieNotFoundException>(() => reviewServices.ListMovieReviews(movieId)); 
        }

        [DataTestMethod]
        [DataRow(5)]
        public void ThrowNotEnoughPermissionsException_WhenUserHasNotLoggedIn(int movieId)
        {
            // Arrange
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
            Assert.ThrowsException<NotEnoughPermissionException>(() => reviewServices.ListMovieReviews(movieId));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(0)]
        public void ThrowArgumentException_WhenParametersAreIncorrect(int movieId)
        {
            // Arrange
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var reviewStub = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();
            var loginStub = new Mock<ILoginSession>();
            loginStub.Setup(l => l.LoggedUserPermissions).Returns(new List<string>() { "some", "things", "listmoviereviews" });

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
            Assert.ThrowsException<ArgumentException>(() => reviewServices.ListMovieReviews(movieId));
        }
    }
}
