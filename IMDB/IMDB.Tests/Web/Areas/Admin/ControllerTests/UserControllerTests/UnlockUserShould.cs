using IMDB.Data.Models;
using IMDB.Web.Areas.Admin.Controllers;
using IMDB.Web.Areas.Admin.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Tests.Web.Areas.Admin.ControllerTests.UserControllerTests
{
    [TestClass]
    public class UnlockUserShould
    {
        [TestMethod]
        public async Task CallCorrectServiceMethod()
        {
            //Arrange
            const string uID = "213123";
            var userManagerMock = new Mock<IUserManager<User>>();
            userManagerMock
                .Setup(umm => umm.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock
                .SetupGet(umm => umm.Users)
                .Returns(new List<User> { new User { Id = uID } }.AsQueryable());

            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = await sut.UnlockUser(new UserModalModelView { ID = uID }) as RedirectToActionResult;
            //Assert
            userManagerMock.Verify(s => s.Users, Times.Once);
            userManagerMock.Verify(s => s.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset>()), Times.Once);
        }

        [TestMethod]
        public async Task ReturnsCorrectViewResult()
        {
            //Arrange
            const string uID = "213123";
            var userManagerMock = new Mock<IUserManager<User>>();
            userManagerMock
                .Setup(umm => umm.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(IdentityResult.Success);
            userManagerMock
                .SetupGet(umm => umm.Users)
                .Returns(new List<User> { new User { Id = uID } }.AsQueryable());

            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = await sut.UnlockUser(new UserModalModelView { ID = uID }) as RedirectToActionResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
        [TestMethod]
        public async Task RedirectWithError_WhenUserIsNull()
        {
            //Arrange
            const string uID = "213123";
            var userManagerMock = new Mock<IUserManager<User>>();
            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = await sut.UnlockUser(new UserModalModelView { ID = uID }) as RedirectToActionResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.IsTrue(sut.StatusMessage.Contains("Error"));
        }
        [TestMethod]
        public async Task Redirect_WhenSetLockoutDateFails()
        {
            //Arrange
            const string uID = "213123";
            var userManagerMock = new Mock<IUserManager<User>>();
            userManagerMock
                .Setup(umm => umm.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTimeOffset>()))
                .ReturnsAsync(IdentityResult.Failed());
            userManagerMock
                .SetupGet(umm => umm.Users)
                .Returns(new List<User> { new User { Id = uID } }.AsQueryable());
            var sut = new UsersController(userManagerMock.Object);
            //Act
            var result = await sut.UnlockUser(new UserModalModelView { ID = uID }) as RedirectToActionResult;
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.IsTrue(sut.StatusMessage.Contains("Error"));
        }
    }
}
