using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Web.Areas.Admin.Models;
using IMDB.Web.Controllers;
using IMDB.Web.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class IndexActionShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethod()
        {
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.GetAllMoviesAsync())
                .ReturnsAsync(new List<Movie>());
            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Index() as ViewResult;
            //Assert
            movieServiceMock.Verify(s => s.GetAllMoviesAsync());
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        { 
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.GetAllMoviesAsync())
                .ReturnsAsync(new List<Movie>());
            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(List<MovieViewModel>));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.GetAllMoviesAsync())
                .ReturnsAsync(new List<Movie>());
            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private Mock<IMovieServices> SetupMockService()
        {
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.GetAllMoviesAsync())
                .ReturnsAsync(new List<Movie>());

            return movieServiceMock;
        }
    }
}
