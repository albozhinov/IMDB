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

namespace IMDB.Tests.Services.MovieServicesTests
{
    [TestClass]
    public class DeleteShould
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void ThrowArgumentException_WhenArgumentsAreIncorrect(int movieID)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();

            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.DeleteMovie(movieID));
        }
        [TestMethod]
        public void ThrowNotEnoughPermissionsException_WhenTheUserIsNotAuthorized()
        {
            // Arrange
            const int movieID = 1;

            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoStub = new Mock<IRepository<Movie>>();
            movieRepoStub
                .Setup(mr => mr.All())
                .Returns(new List<Movie>() { new Movie { ID = movieID, IsDeleted = false } }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionMock = new Mock<ILoginSession>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoStub.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            Assert.ThrowsException<NotEnoughPermissionException>(() => sut.DeleteMovie(movieID));
        }
        [TestMethod]
        public void ThrowsMovieNotFoundException_WhenMovieDoesNotExist()
        {
            // Arrange
            const int movieID = 1;

            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            movieRepoMock
                .Setup(mr => mr.All())
                .Returns(new List<Movie>() { new Movie { ID = movieID, IsDeleted = false } }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();

            var sut = new MovieServices(reviewRepoStub.Object, 
                movieRepoMock.Object, directorRepoStub.Object, 
                genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            Assert.ThrowsException<MovieNotFoundException>(() => sut.DeleteMovie(12312));
        }
        [TestMethod]
        public void ThrowsMovieNotFoundException_WhenMovieIsDeleted()
        {
            // Arrange
            const int movieID = 1;

            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            movieRepoMock
                .Setup(mr => mr.All())
                .Returns(new List<Movie>() { new Movie { ID = movieID, IsDeleted = true } }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            Assert.ThrowsException<MovieNotFoundException>(() => sut.DeleteMovie(movieID));
        }
        [TestMethod]
        public void DeletesAllMovieInformation_WhenArgumentsAreCorrect()
        {
            // Arrange
            const int movieID = 1;
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var review1 = new Review { IsDeleted = false };
            var review2 = new Review { IsDeleted = false };
            var review3 = new Review { IsDeleted = true };
            var movie = new Movie
            {
                ID = movieID,
                IsDeleted = false,
                Reviews = new List<Review>() { review1, review2, review3 }
            };
            movieRepoMock
                .Setup(mr => mr.All())
                .Returns(new List<Movie>() { movie }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act
            sut.DeleteMovie(movieID);
            // Assert
            Assert.IsTrue(movie.IsDeleted);
            Assert.IsTrue(movie.Reviews.All(r => r.IsDeleted));
        }
    }
}
