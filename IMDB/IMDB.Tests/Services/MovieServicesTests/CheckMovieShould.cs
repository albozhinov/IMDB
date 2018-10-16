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
    public class CheckMovieShould
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

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionStub.Object);
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CheckMovie(movieID));
        }
        [TestMethod]
        public void ReturnMoiewViewOfFoundMovie_WhenSuchIsValid()
        {
            //Arrange
            const string movieName = "MovieName";
            const string directorName = "d";
            const int rID1 = 5;
            const int rID2 = 7;
            const string g1 = "g1";
            const string g2 = "g2";
            const string user1 = "u1";
            const string user2 = "u2";
            const int score1 = 3;
            const int score2 = 7;
            const int movieRating1 = 5;
            const int movieRating2 = 7;
            const string text1 = "somereviewtext1";
            const string text2 = "somereviewtext2";

            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movieDirector = new Director { Name = directorName };
            var movieGenre1 = new MovieGenre { Genre = new Genre { GenreType = g1 } };
            var movieGenre2 = new MovieGenre { Genre = new Genre { GenreType = g2 } };
            var review1 = new Review { ID = rID1, User = new User { UserName = user1 }, ReviewScore = score1, MovieRating = movieRating1, Text = text1 };
            var review2 = new Review { ID = rID2, User = new User { UserName = user2 }, ReviewScore = score2, MovieRating = movieRating2, Text = text2 };
            //Should order reviews by their score a.ka reviewScore
            var movieToBeChecked = new Movie
            {
                Name = movieName,
                ID = 1,
                IsDeleted = false,
                Director = movieDirector,
                MovieGenres = new List<MovieGenre>() { movieGenre1, movieGenre2 },
                Reviews = new List<Review>() { review1, review2 }
            };
            review1.Movie = movieToBeChecked;
            review2.Movie = movieToBeChecked;
            movieRepoMock
                 .Setup(mr => mr.All())
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionStub.Object);
            //Act
            var result = sut.CheckMovie(1);
            //Assert
            Assert.IsTrue(result.Director == directorName);
            Assert.IsTrue(result.Name == movieName);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].ByUser == user2);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].Rating == movieRating2);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].MovieName == movieName);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].ReviewID == rID2);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].Score == score2);
            Assert.IsTrue(result.Top5Reviews.ToList()[0].Text == text2);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].ByUser == user1);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].Rating == movieRating1);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].MovieName == movieName);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].ReviewID == rID1);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].Score == score1);
            Assert.IsTrue(result.Top5Reviews.ToList()[1].Text == text1);
        }
        [TestMethod]
        public void ThrowMovieNotFoundException_WhenMovieIsNotFound()
        {
            //Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movieToBeChecked = new Movie
            {
                Name = "Stivi's adventure into unit testing of the underworld",
                ID = 1,
                IsDeleted = false,
            };
            movieRepoMock
                 .Setup(mr => mr.All())
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionStub.Object);
            //Act & Assert
            Assert.ThrowsException<MovieNotFoundException>(() => sut.CheckMovie(52));
        }
        [TestMethod]
        public void ThrowMovieNotFoundException_WhenMovieIsDeleted()
        {
            //Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movieToBeChecked = new Movie
            {
                Name = "Stivi's adventure into unit testing of the underworld",
                ID = 1,
                IsDeleted = true,
            };
            movieRepoMock
                 .Setup(mr => mr.All())
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable());

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var loginSessionStub = new Mock<ILoginSession>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionStub.Object);
            //Act & Assert
            Assert.ThrowsException<MovieNotFoundException>(() => sut.CheckMovie(1));
        }
    }
}
