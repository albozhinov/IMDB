using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IMDB.Tests.Services.MovieServicesTests
{
    [TestClass]
    public class CheckMovieShould
    {
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(int movieID)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CheckMovieAsync(movieID));
        }
        [TestMethod]
        public async Task ReturnMoiewViewOfFoundMovie_WhenSuchIsValid()
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
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable().BuildMock().Object);

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            //Act
            var result = await sut.CheckMovieAsync(1);
            //Assert
            Assert.AreSame(result, movieToBeChecked);
        }
        [TestMethod]
        public async Task ThrowMovieNotFoundException_WhenMovieIsNotFound()
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
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable().BuildMock().Object);

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            //Act & Assert
            await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await sut.CheckMovieAsync(52));
        }
        [TestMethod]
        public async Task ThrowMovieNotFoundException_WhenMovieIsDeleted()
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
                 .Returns(new List<Movie>() { movieToBeChecked }.AsQueryable().BuildMock().Object);

            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();
            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            //Act & Assert
            await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await sut.CheckMovieAsync(1));
        }
    }
}
