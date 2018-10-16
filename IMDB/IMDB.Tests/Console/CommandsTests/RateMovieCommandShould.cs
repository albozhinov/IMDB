using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass]
    public class RateMovieCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenArgumentsAreNull()
        {
            //Arrange
            var movieServicesStub = new Mock<IMovieServices>();

            var sut = new RateMovieCommand(movieServicesStub.Object);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
        }
        [DataTestMethod]
        [DataRow(new string[] { "1", "2", "3", "4" })]
        [DataRow(new string[] { "1", "notanumber", "thatsoptional" })]
        [DataRow(new string[] { "notanumber", "2", "thatsoptional" })]
        [DataRow(new string[] { "arg1" })]
        [DataRow(new string[] { })]
        public void ReturnsWrongSyntax_WhenArgumentsFormatIsInccorect(string[] arg)
        {
            //Arrange
            var movieServicesStub = new Mock<IMovieServices>();

            var sut = new RateMovieCommand(movieServicesStub.Object);
            //Act
            var result = sut.Run(new List<string>(arg));
            //Assert
            Assert.IsTrue(result.Contains("wrong", StringComparison.CurrentCultureIgnoreCase));
        }
        [DataTestMethod]
        [DataRow(new string[] { "1", "2", "optionalcommant" })]
        [DataRow(new string[] { "1", "2", null })]
        public void ReturnsSuccess_WhenArgumentsAreCorrect(string[] arg)
        {
            // format of CMD arguments:  <movieID> : <rating> : <optional: text>
            //Arrange
            var movieServicesMock = new Mock<IMovieServices>();

            var sut = new RateMovieCommand(movieServicesMock.Object);
            //Act
            var result = sut.Run(new List<string>(arg));
            //Assert
            Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(arg[0], StringComparison.CurrentCultureIgnoreCase));
            movieServicesMock.Verify(msm => msm.RateMovie(Convert.ToInt32(arg[0]), Convert.ToDouble(arg[1]), arg[2]));
        }
    }
}
