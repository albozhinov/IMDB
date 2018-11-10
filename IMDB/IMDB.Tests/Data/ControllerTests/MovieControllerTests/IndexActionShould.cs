using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Tests.Data.ControllerTests.MovieControllerTests
{
    [TestClass]
    public class IndexActionShould
    {
        [TestMethod]
        public void CallCorrectServiceMethod()
        {
            //Arrange
            var serviceMock = this.SetupMockService();
            //var sut = new MovieController(serviceMock)
        }
        [TestMethod]
        public void ReturnCorrectViewModel()
        {

        }
        [TestMethod]
        public void ReturnsCorrectViewResult()
        {

        }

        private Mock<IMovieServices> SetupMockService()
        {
            var movieServiceMock = new Mock<IMovieServices>();
            movieServiceMock
                .Setup(ms => ms.GetAllMovies())
                .Returns(new List<Movie>());

            return movieServiceMock;
        }

    }
}
