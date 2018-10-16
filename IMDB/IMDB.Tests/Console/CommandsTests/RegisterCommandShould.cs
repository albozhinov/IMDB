using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass
        ]
    public class RegisterCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenArgumentsAreNull()
        {
            //Arrange
            var userServicesStub = new Mock<IUserServices>();

            var sut = new RegisterCommand(userServicesStub.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
        }
        [DataTestMethod]
        [DataRow(new string[] { "1", "2", "dwa" })]
        [DataRow(new string[] { "dawd" })]
        [DataRow(new string[] { })]
        public void ReturnsWrongSyntax_WhenArgumentsFormatIsInccorect(string[] arg)
        {
            //Arrange
            var userServicesStub = new Mock<IUserServices>();

            var sut = new LoginCommand(userServicesStub.Object);
            //Act
            var result = sut.Run(new List<string>(arg));
            //Assert
            Assert.IsTrue(result.Contains("wrong", StringComparison.CurrentCultureIgnoreCase));
        }
        [TestMethod]
        public void ReturnsSuccess_WhenArgumentsFormatIsCcorect()
        {
            const string arg1 = "uname";
            const string arg2 = "pass";
            //Arrange
            var userServicesMock = new Mock<IUserServices>();

            var sut = new RegisterCommand(userServicesMock.Object);
            //Act
            var result = sut.Run(new List<string> { arg1, arg2 });
            //Assert
            Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(arg1, StringComparison.CurrentCultureIgnoreCase));
            userServicesMock.Verify(msm => msm.Register(arg1, arg2), Times.Once);
        }
    }
}
