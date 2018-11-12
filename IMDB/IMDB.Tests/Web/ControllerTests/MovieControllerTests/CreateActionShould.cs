using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Web.Controllers;
using IMDB.Web.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class CreateActionGetShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethodWithCorrectParams()
        {
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(msm => msm.GetGenresAsync())
                .ReturnsAsync(new List<Genre>());

                var cache = new MemoryCache(new MemoryCacheOptions());
                var userManagerMock = new Mock<IUserManager<User>>();

            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            await sut.Create();
           //Assert
            movieServiceMock.Verify(msm => msm.GetGenresAsync(), Times.Once);
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
               .Setup(msm => msm.GetGenresAsync())
               .ReturnsAsync(new List<Genre>());
            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();

            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Create() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(MovieViewModel));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
               .Setup(msm => msm.GetGenresAsync())
               .ReturnsAsync(new List<Genre>());
            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Create() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private Mock<IMovieServices> SetupMockService()
        {
            var genres = new List<string> { "Action" };
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CreateMovieAsync("movieName", genres, "Tosho"))
                .ReturnsAsync(new Movie());

            return movieServiceMock;
        }
    }
}
