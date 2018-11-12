using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Tests.Services.ReviewServicesTests
{
    [TestClass]
    public class ListMovieReviewsShould
    {
        [DataTestMethod]
        [DataRow(5, false)]
        [DataRow(1, true)]
        public async Task ThrowMovieNotFoundException_WhenMovieNotFound(int movieId, bool isDeleted)
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

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable().BuildMock().Object;   
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object); 

            // Act and Assert
            await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await reviewServices.ListMovieReviewsAsync(movieId)); 
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(0)]
        public async Task ThrowArgumentException_WhenParametersAreIncorrect(int movieId)
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

            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable().BuildMock().Object;
            movieRepoMock.Setup(m => m.All()).Returns(allMoviesMock);

            var reviewServices = new ReviewsService(reviewStub.Object, movieRepoMock.Object, reviewRatingsStub.Object);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await reviewServices.ListMovieReviewsAsync(movieId));
        }

        [TestMethod]        
        public async Task ReturnMovieReviewsView_WhenParameterIsCorrect()
        {
            // Arrange
            const int movieID = 1;
            var movieRepoStub = new Mock<IRepository<Movie>>();
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

            var movieMock = new Movie(){ ID = 1, Name = "Rambo", IsDeleted = false };
            var allMoviesMock = new List<Movie>() { movieMock }.AsQueryable().BuildMock().Object;
            movieRepoStub.Setup(m => m.All()).Returns(allMoviesMock);            

            var user1 = new User() { UserName = "Zaprqn" };
            var review1 = new Review() { ID = 1, MovieRating = 8.99, ReviewScore = 7.5, Text = "Test ReviewView model", User = user1, Movie = movieMock, NumberOfVotes = 35, MovieID = movieMock.ID };

            var user2 = new User() { UserName = "Stamat" };
            var movieMock2 = new Movie() { ID = 2, Name = "Jackass", IsDeleted = false };
            var review2 = new Review() { ID = 2, MovieRating = 4.5, ReviewScore = 7.5, Text = "Test ReviewView model", User = user2, Movie = movieMock2, NumberOfVotes = 23, MovieID = movieMock2.ID };

            var allReviews = new List<Review>() { review1, review2 }.AsQueryable().BuildMock().Object;
            reviewRepoMock.Setup(r => r.All()).Returns(allReviews);

            var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

            // Act 
            var result = await reviewServices.ListMovieReviewsAsync(movieID);

			//Assert
			Assert.AreSame(review1, result.First());
		}
    }
}
