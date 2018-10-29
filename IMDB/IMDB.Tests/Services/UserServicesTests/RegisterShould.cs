//using IMDB.Data.Models;
//using IMDB.Data.Repository;
//using IMDB.Services;
//using IMDB.Services.Contracts;
//using IMDB.Services.Exceptions;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IMDB.Tests.Services.UserServicesTests
//{
//    [TestClass]
//    public class RegisterShould
//    {
//        [DataTestMethod]
//        [DataRow(null, "someRandomPassword")]
//        [DataRow("someRandomUserName", null)]
//        public void ThrowRegisterFailedException_WhenArgumentsAreNull(string userName, string password)
//        {
//            // Arrange
//            var loginSessionStub = new Mock<ILoginSession>();
//            var userRepoStub = new Mock<IRepository<User>>();
//            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
//            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

//            // Act & Assert
//            Assert.ThrowsException<RegisterFailedException>(() => sut.Register(userName, password));
//        }
//        [DataTestMethod]
//        [DataRow("user name", "someRandomPassword")]
//        [DataRow("someRandomUserName", "pass word")]
//        [DataRow("a", "valid")]
//        [DataRow("valid", "aa")]
//        [DataRow("valid", "thisissooooolongandinvalid")]
//        [DataRow("thisissooooolongandinvalid", "valid")]
//        public void ThrowRegisterFailedException_WhenArgumentsFormatIsIncorrect(string userName, string password)
//        {
//            // Arrange
//            var loginSessionStub = new Mock<ILoginSession>();
//            var userRepoStub = new Mock<IRepository<User>>();
//            var permissionsRepoStub = new Mock<IRepository<Permissions>>();
//            var sut = new UserServices(loginSessionStub.Object, userRepoStub.Object, permissionsRepoStub.Object);

//            // Act & Assert
//            Assert.ThrowsException<ArgumentException>(() => sut.Register(userName, password));
//        }
//        [TestMethod]
//        public void ThrowRegisterFailedException_WhenUserAlreadyExists()
//        {
//            // Arrange
//            const string userName = "stivi";
//            var loginSessionStub = new Mock<ILoginSession>();
//            var userRepoMock = new Mock<IRepository<User>>();
//            var user = new User
//            {
//                UserName = userName,
//                Password = "A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3".ToLower() //hash for 123
//            };
//            userRepoMock
//                .Setup(ur => ur.All())
//                .Returns(new List<User>() { user }.AsQueryable());
//            var permissionsRepoStub = new Mock<IRepository<Permissions>>();

//            var sut = new UserServices(loginSessionStub.Object, userRepoMock.Object, permissionsRepoStub.Object);
//            //Act & Assert
//            Assert.ThrowsException<RegisterFailedException>(() => sut.Register(userName, "somepass"));
//        }
//        [TestMethod]
//        public void RegisterUser_WhenArgumentsAreCorrect()
//        {
//            // Arrange
//            const string userName = "stivi";
//            var loginSessionStub = new Mock<ILoginSession>();

//            var userRepoMock = new Mock<IRepository<User>>();
//            User createdUser = null;
//            userRepoMock
//                .Setup(ur => ur.Add(It.IsAny<User>()))
//                .Callback<User>(u => createdUser = u);

//            var permissionsRepoStub = new Mock<IRepository<Permissions>>();

//            var sut = new UserServices(loginSessionStub.Object, userRepoMock.Object, permissionsRepoStub.Object);
//            //Act
//            sut.Register(userName, "1234");
//            //Assert
//            Assert.IsTrue(createdUser.Password == "03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4".ToLower()); //hash for 1234
//            Assert.IsTrue(createdUser.UserName == userName);
//            Assert.IsTrue(createdUser.Rank == (int)UserRoles.User);
//            userRepoMock.Verify(ur => ur.Save(), Times.Once);
//        }
//    }
//}
