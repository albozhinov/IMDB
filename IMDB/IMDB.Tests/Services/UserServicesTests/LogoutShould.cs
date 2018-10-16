using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Tests.Services.UserServicesTests
{
    [TestClass]
    public class LogoutShould
    {
        [TestMethod]
        public void ThrowNotEnoughPermissionsException_WhenUserHasNotLoggedIn()
        {
            // Arrange
            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() {"cmd0", "cmd1", "butnotcmdlogout" });
            var userRepoStub = new Mock<IRepository<User>>();
            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var sut = new UserServices(loginSessionMock.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act & Assert
            Assert.ThrowsException<NotEnoughPermissionException>(() => sut.Logout());
        }
        [TestMethod]
        public void Logout_WhenPermissionsSuffice()
        {
            // Arrange
            const string p0Text = "p0";

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "logout" });
            var userRepoStub = new Mock<IRepository<User>>();

            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var p0 = new Permissions { Rank = 0, Text = p0Text };
            var p1 = new Permissions { Rank = 1, Text = "p1" };
            var p2 = new Permissions { Rank = 2, Text = "p2" };
            permissionsRepoStub
                .Setup(p => p.All())
                .Returns(new List<Permissions> { p0, p1, p2 }.AsQueryable());

            var sut = new UserServices(loginSessionMock.Object, userRepoStub.Object, permissionsRepoStub.Object);
            //Act
            sut.Logout();
            //Assert
            loginSessionMock.VerifySet(ls => ls.LoggedUserPermissions = new List<string> { p0Text });
            loginSessionMock.VerifySet(ls => ls.LoggedUserRole = UserRoles.Guest);
        }
    }
}
