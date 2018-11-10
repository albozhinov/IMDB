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
    class SearchActionShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethod()
        {
            //Arrange
            var model = new SearchViewModel();
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model) as ViewResult;
            //Assert
            serviceMock.Verify(s => s.SearchMovieAsync("Venom", "", ""));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            //Arrange
            var model = new SearchViewModel();
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model) as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(SearchViewModel));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var model = new SearchViewModel();
            var serviceMock = this.SetupMockService();
            var movieCachMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(serviceMock.Object, movieCachMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model) as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }


        private Mock<IMovieServices> SetupMockService()
        {
            var genres = new List<string> { "Action" };
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.SearchMovieAsync("Venom", "", ""))
                .ReturnsAsync(new List<Movie>());

            return movieServiceMock;
        }
    }
}
