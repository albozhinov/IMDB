using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass]
    public class CreateMovieCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenArgumentsAreNull()
        {
            //Arrange
            var movieServicesStub = new Mock<IMovieServices>();

            var sut = new CreateMovieCommand(movieServicesStub.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
        }
        [DataTestMethod]
        [DataRow(new string[] { "a1", "a2" })]
        [DataRow(new string[] { "a1" })]
        [DataRow(new string[] { })]
        public void ReturnsWrongSyntax_WhenArgumentsFormatIsInccorect(string[] arg)
        {
            //Arrange
            var movieServicesStub = new Mock<IMovieServices>();

            var sut = new CreateMovieCommand(movieServicesStub.Object);
            //Act
            var result = sut.Run(new List<string>(arg));
            //Assert
            Assert.IsTrue(result.Contains("wrong", StringComparison.CurrentCultureIgnoreCase));
        }
        [TestMethod]
        public void ReturnsSuccess_WhenArgumentsFormatIsCcorect()
        {
            //Arrange
            var movieServicesStub = new Mock<IMovieServices>();

            var sut = new CreateMovieCommand(movieServicesStub.Object);
            //Act
            var result = sut.Run(new List<string> {"t1", "t2", "t3"});
            //Assert
            Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
