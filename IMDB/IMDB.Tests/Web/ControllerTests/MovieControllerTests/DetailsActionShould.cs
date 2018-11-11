using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Web.Controllers;
using IMDB.Web.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class DetailsActionShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethod()
        {
            //Arrange
            const int movieID = 3;
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(movieID))
                .ReturnsAsync(new Movie { Director = new Director(), MovieGenres = new List<MovieGenre>(), Reviews = new List<Review>() });

            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Details(movieID) as ViewResult;
            //Assert
            movieServiceMock.Verify(s => s.CheckMovieAsync(movieID), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            //Arrange
            const int movieID = 3;
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(movieID))
                .ReturnsAsync(new Movie { Director = new Director(), MovieGenres = new List<MovieGenre>(), Reviews = new List<Review>() });

            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Details(movieID) as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(MovieViewModel));

        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            const int movieID = 3;
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(movieID))
                .ReturnsAsync(new Movie { Director = new Director(), MovieGenres = new List<MovieGenre>(), Reviews = new List<Review>() });

            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Details(movieID);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public async Task Return404_WhenMovieNotFoundExceptionIsThrown()
        {
            //Arrange
            const int movieID = 3;
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(movieID))
                .Callback(() => throw new MovieNotFoundException());

            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Details(movieID);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task Return404_WhenArgumentExceptionIsThrown()
        {
            //Arrange
            const int movieID = 3;
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(movieID))
                .Callback(() => throw new ArgumentException());

            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Details(movieID);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
