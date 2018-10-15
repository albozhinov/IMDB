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
        [DataRow(5, 5.00)]
        [DataRow(1, 5.00)]
        public void ThrowMovieNotFoundException_WhenMovieNotFound(int movieId, double rating)
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
            Assert.ThrowsException<ReviewNotFoundException>(() => reviewServices.RateReview(movieId, rating)); 
        }




    }
}
