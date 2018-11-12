using IMDB.Data.Models;
using IMDB.Services.Contracts;
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
using X.PagedList;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class CreateActionPostShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethodWithCorrectParams()
        {
            //Arrange
            const string movieName = "Venom";
            const string movieDirector = "director";
            var genreList = new List<string> { null };
            var newMovie = new Movie();

            var model = new MovieViewModel() { Name = movieName, Genres = genreList, Director = movieDirector };
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CreateMovieAsync(movieName, genreList, movieDirector))
                .ReturnsAsync(newMovie);

            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();

            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Create(model) as ViewResult;
            //Assert
            movieServiceMock.Verify(s => s.CreateMovieAsync(movieName, genreList, movieDirector));
        }


        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var model = new MovieViewModel();
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.CreateMovieAsync(null, null, null))
                .ReturnsAsync(new Movie());

            var cache = new MemoryCache(new MemoryCacheOptions());
            var userManagerMock = new Mock<IUserManager<User>>();

            var sut = new MovieController(movieServiceMock.Object, cache, userManagerMock.Object);
            //Act
            var result = await sut.Create(model);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
