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

            var movieMock = new Movie()
            {
                ID = 1,
                Name = "Rambo",
                IsDeleted = isDeleted
            };           

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();   
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object); 

            // Act and Assert
            Assert.ThrowsException<MovieNotFoundException>(() => reviewServices.ListMovieReviews(movieId)); 
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

            var movieMock = new Movie()
            {
                ID = 1,
                Name = "Rambo",
                IsDeleted = true
            };

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => reviewServices.ListMovieReviews(movieId));
        }

        [TestMethod]        
        public void ReturnMovieReviewsView_WhenParameterIsCorrect()
        {
            // Arrange
            const int movieID = 1;
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

            var movieMock = new Movie(){ ID = 1, Name = "Rambo", IsDeleted = false };
            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable();
            movieRepoStub.Setup(m => m.All()).Returns(allMoviesMock);            

            var user1 = new User() { UserName = "Zaprqn" };
            var review1 = new Review() { ID = 1, MovieRating = 8.99, ReviewScore = 7.5, Text = "Test ReviewView model", User = user1, Movie = movieMock, NumberOfVotes = 35, MovieID = movieMock.ID };

            var user2 = new User() { UserName = "Stamat" };
            var movieMock2 = new Movie() { ID = 2, Name = "Jackass", IsDeleted = false };
            var review2 = new Review() { ID = 2, MovieRating = 4.5, ReviewScore = 7.5, Text = "Test ReviewView model", User = user2, Movie = movieMock2, NumberOfVotes = 23, MovieID = movieMock2.ID };

            var allReviews = new List<Review>() { review1, review2 }.AsQueryable();
            reviewRepoMock.Setup(r => r.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act 
            var result = reviewServices.ListMovieReviews(movieID);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.IsTrue(result.First().ByUser == review1.User.UserName);
            Assert.IsTrue(result.First().ReviewID == review1.ID);
            Assert.IsTrue(result.First().Rating == review1.MovieRating);
            Assert.IsTrue(result.First().Score == review1.ReviewScore);
            Assert.IsTrue(result.First().Text == review1.Text);
            Assert.IsTrue(result.First().MovieName == review1.Movie.Name);
            Assert.IsTrue(result.First().NumberOfVotes == review1.NumberOfVotes);
        }
    }
}
