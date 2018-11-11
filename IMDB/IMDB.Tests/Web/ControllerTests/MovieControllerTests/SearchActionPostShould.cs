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
using X.PagedList;

namespace IMDB.Tests.Web.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class SearchActionPostShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethodWithCorrectParams()
        {
            //Arrange
            const string movieName = "Venom";
            var genreList = new List<string> { null };
            var model = new SearchViewModel() {Name = movieName,Genres = genreList };
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.SearchMovieAsync(movieName, genreList, null))
                .ReturnsAsync(new List<Movie>());

            var movieCacheMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCacheMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model, null) as ViewResult;
            //Assert
            movieServiceMock.Verify(s => s.SearchMovieAsync(movieName,genreList, null));
        }

        [TestMethod]
        public async Task ReturnCorrectViewModel()
        {
            //Arrange
            var model = new SearchViewModel();
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.SearchMovieAsync(null, null, null))
                .ReturnsAsync(new List<Movie>());
            var movieCacheMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCacheMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model, null) as PartialViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(IPagedList<MovieViewModel>));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            var model = new SearchViewModel();
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.SearchMovieAsync(null, null, null))
                .ReturnsAsync(new List<Movie>());

            var movieCacheMock = new Mock<IMemoryCache>();
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new MovieController(movieServiceMock.Object, movieCacheMock.Object, userManagerMock.Object);
            //Act
            var result = await sut.Search(model, null);
            //Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }
    }
}
