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
			const string arg1 = "arg1";
			const string arg2 = "arg2";
			const string arg3 = "arg3";
			const string arg4 = "arg4";
			//Arrange
			var movieServicesMock = new Mock<IMovieServices>();

            var sut = new CreateMovieCommand(movieServicesMock.Object);
            //Act
            var result = sut.Run(new List<string> {arg1, arg2, arg3, arg4});
            //Assert
            Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
			movieServicesMock.Verify(msm => msm.CreateMovie(arg1, new List<string>() { arg3, arg4 }, arg2), Times.Once);
        }
    }
}
