using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Web.Areas.Admin.Controllers;
using IMDB.Web.Areas.Admin.Models;
using IMDB.Web.Controllers;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Tests.Web.Areas.Admin.ControllerTests.UserControllerTests
{
    [TestClass]
    public class IndexActionShould
    {
        [TestMethod]
        public void CallCorrectServiceMethod()
        {
            //Arrange
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = sut.Index() as ViewResult;
            //Assert
            userManagerMock.Verify(s => s.Users, Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectViewModel()
        {
            //Arrange
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new UsersController(userManagerMock.Object);
            //Acts
            var result = sut.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(IndexViewModel));
        }

        [TestMethod]
        public void ReturnsCorrectViewResult()
        {
            //Arrange
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = sut.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
