using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
	[TestClass]
	public class DeleteMovieCommandShould
	{
		[TestMethod]
		public void ThrowArgumentNullException_WhenArgumentsAreNull()
		{
			//Arrange
			var movieServicesStub = new Mock<IMovieServices>();

			var sut = new DeleteMovieCommand(movieServicesStub.Object);
			//Act & Assert
			Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
		}
		[DataTestMethod]
		[DataRow(new string[] { "1", "2" })]
		[DataRow(new string[] { "textnotnumber" })]
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
			const int movieID = 12;
			var movieServicesMock = new Mock<IMovieServices>();

			var sut = new DeleteMovieCommand(movieServicesMock.Object);
			//Act
			var result = sut.Run(new List<string> { movieID.ToString() });
			//Assert
			Assert.IsTrue(result.Contains("success", StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(movieID.ToString(), StringComparison.CurrentCultureIgnoreCase));
			movieServicesMock.Verify(msm => msm.DeleteMovie(movieID), Times.Once);
		}
	}
}
