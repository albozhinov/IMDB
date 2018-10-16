using IMDB.Console.Commands;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Console.CommandsTests
{
	[TestClass]
	public class CheckMovieCommandShould
	{
		[TestMethod]
		public void ThrowArgumentNullException_WhenArgumentsAreNull()
		{
			//Arrange
			var movieServicesStub = new Mock<IMovieServices>();

			var sut = new CheckMovieCommand(movieServicesStub.Object);
			//Act & Assert
			Assert.ThrowsException<ArgumentNullException>(() => sut.Run(null));
		}
		[DataTestMethod]
		[DataRow(new string[] { "1", "2" })]
		[DataRow(new string[] { "textnotnumber"})]
		[DataRow(new string[] { })]
		public void ReturnsWrongSyntax_WhenArgumentsFormatIsInccorect(string[] arg)
		{
			//Arrange
			var movieServicesStub = new Mock<IMovieServices>();

			var sut = new CheckMovieCommand(movieServicesStub.Object);
			//Act
			var result = sut.Run(new List<string>(arg));
			//Assert
			Assert.IsTrue(result.Contains("wrong", StringComparison.CurrentCultureIgnoreCase));
		}
		[TestMethod]
		public void CallsToStringOnView_WhenArgumentsFormatIsCcorect()
		{
			//Arrange
            var movieViewMock = new Mock<MovieView>(); 
			var movieServicesMock = new Mock<IMovieServices>();
			movieServicesMock
				.Setup(msm => msm.CheckMovie(It.IsAny<int>()))
				.Returns(movieViewMock.Object);

			var sut = new CheckMovieCommand(movieServicesMock.Object);
			//Act
			var result = sut.Run(new List<string> { "1" });
			//Assert
            movieViewMock.Verify(mvm => mvm.ToString(), Times.Once);
		}
        [TestMethod]
        public void CallsService_WhenArgumentsFormatIsCorrect()
        {
            //Arrange
            var movieViewMock = new Mock<MovieView>();
            var movieServicesMock = new Mock<IMovieServices>();
            movieServicesMock
                .Setup(msm => msm.CheckMovie(It.IsAny<int>()))
                .Returns(movieViewMock.Object);

            var sut = new CheckMovieCommand(movieServicesMock.Object);
            //Act
            var result = sut.Run(new List<string> { "1" });
            //Assert
            movieServicesMock.Verify(msm => msm.CheckMovie(1), Times.Once);
        }
	}
}
