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
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Tests.Data.ControllerTests.MovieControllerTests
{
    [TestClass]
    class DetailsActionShould
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
            var result = await sut.Details(3) as ViewResult;
            //Assert
            serviceMock.Verify(s => s.CheckMovieAsync(3), Times.Once);

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
            var result = await sut.Details(3) as ViewResult;
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
            var result = await sut.Details(3) as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(MovieViewModel));
        }

        private Mock<IMovieServices> SetupMockService()
        {
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CheckMovieAsync(3))
                .ReturnsAsync(new Movie());

            return movieServiceMock;
        }
    }
}
