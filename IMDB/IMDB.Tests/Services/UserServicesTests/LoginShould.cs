using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Tests.Services.UserServicesTests
{
    [TestClass]
    public class LoginShould
    {
        [DataTestMethod]
        [DataRow(null, "someRandomPassword")]
        [DataRow("someRandomUserName", null)]
        public void ThrowLoginFailedException_WhenArgumentsAreNull(string userName, string password)
        {
            // Arrange
            var loginSessionStub = new Mock<ILoginSession>();
            var userRepoStub = new Mock<IRepository<User>>();
            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act & Assert
            Assert.ThrowsException<LoginFailedException>(() => sut.Login(userName, password));
        }
        [DataTestMethod]
        [DataRow("user name", "someRandomPassword")]
        [DataRow("someRandomUserName", "pass word")]
        public void ThrowLoginFailedException_WhenArgumentsContainEmptySpaces(string userName, string password)
        {
            // Arrange
            var loginSessionStub = new Mock<ILoginSession>();
            var userRepoStub = new Mock<IRepository<User>>();
            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.Login(userName, password));
        }
        [TestMethod]
        public void ThrowLoginFailedException_WhenUserIsNotFound()
        {
            // Arrange
            var loginSessionStub = new Mock<ILoginSession>();
            var userRepoStub = new Mock<IRepository<User>>();
            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act & Assert
            Assert.ThrowsException<LoginFailedException>(() => sut.Login("Stivi", "somePassword"));
        }
        [TestMethod]
        public void ThrowLoginFailedException_WhenPasswordIsWrong()
        {
            // Arrange
            var loginSessionStub = new Mock<ILoginSession>();
            var userRepoStub = new Mock<IRepository<User>>();
            var user = new User
            {
                UserName = "stivi",
                Password = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3".ToLower() //hash for 123
            };
            userRepoStub
                .Setup(ur => ur.All())
                .Returns(new List<User>() { user }.AsQueryable());
            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act & Assert
            Assert.ThrowsException<LoginFailedException>(() => sut.Login("stivi", "12"));
        }
        [TestMethod]
        public void LogIn_WhenLoginDataIsCorrect()
        {
            // Arrange
            const int ID = 1;
            const int Rank = 1; //user
            const string p0Text = "p0";
            const string p1Text = "p1";

            var loginSessionMock = new Mock<ILoginSession>();

            var userRepoStub = new Mock<IRepository<User>>();
            var user = new User
            {
                ID = ID,
                Rank = Rank,
                UserName = "stivi",
                Password = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3".ToLower() //hash for 123
            };
            userRepoStub
                .Setup(ur => ur.All())
                .Returns(new List<User>() { user }.AsQueryable());

            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
            var p0 = new Permissions { Rank = 0, Text = p0Text};
            var p1 = new Permissions { Rank = 1, Text = p1Text };
            var p2 = new Permissions { Rank = 2, Text = "p2" };
            permissionsRepoStub
                .Setup(p => p.All())
                .Returns(new List<Permissions> { p0, p1, p2}.AsQueryable());

            var sut = new UserServices(loginSessionMock.Object, userRepoStub.Object, permissionsRepoStub.Object);

            // Act
            sut.Login("stivi", "123");
            //Assert
            loginSessionMock.VerifySet(ls => ls.LoggedUserID = ID);
            loginSessionMock.VerifySet(ls => ls.LoggedUserRole = (UserRoles)Rank);
            loginSessionMock.VerifySet(ls => ls.LoggedUserPermissions = new List<string> { p0Text, p1Text });
        }
    }
}
