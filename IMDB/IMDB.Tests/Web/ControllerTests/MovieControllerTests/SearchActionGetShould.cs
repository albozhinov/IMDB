using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Web.Controllers;
using IMDB.Web.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class SearchActionGetShould
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
            await sut.Search();
            await sut.Search();
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
            var result = await sut.Search() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(SearchViewModel));
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
            var result = await sut.Search();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
