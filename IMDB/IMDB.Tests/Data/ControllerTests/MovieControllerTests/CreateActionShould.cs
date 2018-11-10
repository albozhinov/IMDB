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

namespace IMDB.Tests.Data.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class CreateActionShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethod()
        {
            //Arrange
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Create() as ViewResult;
            //Assert
            serviceMock.Verify(s => s.CreateMovieAsync("movieName", new List<string> { "Action" }, "Tosho"));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            //Arrange
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Create() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(MovieViewModel));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
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
