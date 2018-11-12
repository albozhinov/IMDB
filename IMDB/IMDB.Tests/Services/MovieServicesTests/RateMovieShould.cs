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
using System.Threading.Tasks;

namespace IMDB.Tests.Services.MovieServicesTests
{
    [TestClass]
    public class RateMovieShould
    {
        [DataTestMethod]
        [DataRow(-1, 1)]
        [DataRow(0, 1)]
        [DataRow(2, -1)]
        [DataRow(2, 11)]
        public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(int movieID, int rating)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.RateMovieAsync(movieID, rating, "pishki", "randomUserId"));
        }
        [DataTestMethod]
        [DataRow(1, true)]
        [DataRow(2, false)]
        public async Task ThrowMovieNotFoundExceptionWhenMovieIsNotValid(int movieID, bool deletedFlag)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movieMock = new Movie
            {
                Name = "Stivi's adventure into unit testing of the underworld",
                ID = 1,
                IsDeleted = deletedFlag,
            };
            movieRepoMock
                 .Setup(mr => mr.All())
                 .Returns(new List<Movie>() { movieMock }.AsQueryable().BuildMock().Object);

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await sut.RateMovieAsync(movieID, 5, "pishki", "randomUseRId"));
        }
        [TestMethod]
        public async Task UpdateMovieScoreCreatingReview_WhenParametersAreCorrect()
        {
            // Arrange
            const int movieID = 2;
            const int ratingInput = 8;
            const string textInput = "pishki";
            const string userId = "213qwas";
            const int r1rating = 7;
            const int r2rating = 3;
            const int r3DeleteDrating = 3;
            const int movieScore = 5;


            var movieRepoMock = new Mock<IRepository<Movie>>();
            var review1 = new Review { MovieID = movieID, MovieRating = r1rating, UserID = "123123123" };
            var review2 = new Review { MovieID = movieID, MovieRating = r2rating, UserID = "12352315" };
            var review3Deleted = new Review { MovieID = movieID, MovieRating = r3DeleteDrating, IsDeleted = true };
            var revireList = new List<Review>() { review1, review2, review3Deleted };
            var movie = new Movie
            {
                Name = "Stivi's adventure into unit testing of the underworld",
                ID = movieID,
                IsDeleted = false,
                MovieScore = movieScore,
                Reviews = revireList,
                NumberOfVotes = 2
            };
            movieRepoMock
                 .Setup(mr => mr.All())
                 .Returns(new List<Movie>() { movie }.AsQueryable().BuildMock().Object);

            var reviewRepoMock = new Mock<IRepository<Review>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoMock.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act
            await sut.RateMovieAsync(movieID, ratingInput, textInput, userId);
            //Assert
            //check if movie's score is updated should be 6
            Assert.IsTrue(movie.MovieScore == (r1rating + r2rating + ratingInput) / movie.NumberOfVotes);
            Assert.IsTrue(movie.Reviews.Last().MovieID == movieID);
            Assert.IsTrue(movie.Reviews.Last().MovieRating == ratingInput);
            Assert.IsTrue(movie.Reviews.Last().Text == textInput);
            movieRepoMock.Verify(mr => mr.Update(movie));
            movieRepoMock.Verify(mr => mr.SaveAsync(), Times.Once);
            movieRepoMock.Verify(rrm => rrm.SaveAsync(), Times.Once);
        }
        //[TestMethod]
        //public async Task UpdateMovieScoreUpdatingReview_WhenParametersAreCorrect()
        //{
        //    // Arrange
        //    const int movieID = 2;
        //    const string loggedUserID = "12123123";
        //    const int ratingInput = 8;
        //    const string textInput = "pishki";
        //    const int r1rating = 7;
        //    const int r2rating = 3;
        //    const int r3DeleteDrating = 3;
        //    const int movieScore = 5;


        //    var movieRepoMock = new Mock<IRepository<Movie>>();
        //    var review1ToBeUpdated = new Review { MovieID = movieID, MovieRating = r1rating, UserID = loggedUserID };
        //    var review2 = new Review { MovieID = movieID, MovieRating = r2rating, UserID = "124124" };
        //    var review3Deleted = new Review { MovieID = movieID, MovieRating = r3DeleteDrating, IsDeleted = true };
        //    var reviewList = new List<Review>() { review1ToBeUpdated, review2, review3Deleted };
        //    var movie = new Movie
        //    {
        //        Name = "Stivi's adventure into unit testing of the underworld",
        //        ID = movieID,
        //        IsDeleted = false,
        //        MovieScore = movieScore,
        //        Reviews = reviewList,
        //        NumberOfVotes = 2
        //    };
        //    movieRepoMock
        //         .Setup(mr => mr.All())
        //         .Returns(new List<Movie>() { movie }.AsQueryable().BuildMock().Object);

        //    var reviewRepoMock = new Mock<IRepository<Review>>();
        //    var directorRepoStub = new Mock<IRepository<Director>>();
        //    var genreRepoStub = new Mock<IRepository<Genre>>();
        //    var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

        //    var sut = new MovieServices(reviewRepoMock.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
        //    // Act
        //    await sut.RateMovieAsync(movieID, ratingInput, textInput, loggedUserID);
        //    //Assert
        //    //check if movie's score is updated should be 6
        //    Assert.IsTrue(movie.MovieScore == (double)(r1rating - r1rating + r2rating + ratingInput) / movie.NumberOfVotes);
        //    Assert.IsTrue(review1ToBeUpdated.MovieID == movieID);
        //    Assert.IsTrue(review1ToBeUpdated.MovieRating == ratingInput);
        //    Assert.IsTrue(review1ToBeUpdated.UserID == loggedUserID);
        //    Assert.IsTrue(review1ToBeUpdated.Text == textInput);
        //    movieRepoMock.Verify(mr => mr.Update(movie));
        //    movieRepoMock.Verify(mr => mr.SaveAsync(), Times.Once);
        //}
    }
}