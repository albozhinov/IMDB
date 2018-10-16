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
		public void ReturnsServiceResultAsString_WhenArgumentsFormatIsCcorect()
		{
			//Arrange
			const string movieName = "somestring";
			const string movieDirector = "director";
			const string genre1 = "genre1";
			const string genre2 = "genre2";
			const int score = 8;
			const int numberOfVotes = 92;
			const int reviewRating = 7;
			const string reviewText = "t1";
			var reviewView = new ReviewView { Rating = reviewRating, Text = reviewText };
			var movieView = new MovieView()
			{
				Name = movieName,
				Director = movieDirector,
				Genres = new List<string> { genre1, genre2 },
				Score = score,
				NumberOfVotes = numberOfVotes,
				Top5Reviews = new List<ReviewView> { reviewView } 
			};
			var movieServicesMock = new Mock<IMovieServices>();
			movieServicesMock
				.Setup(msm => msm.CheckMovie(It.IsAny<int>()))
				.Returns(movieView);

			var sut = new CheckMovieCommand(movieServicesMock.Object);
			//Act
			var result = sut.Run(new List<string> { "1" });
			//Assert
			Assert.IsTrue(result.Contains(movieName, StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(movieDirector, StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(genre1, StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(genre2, StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(reviewText, StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(reviewRating.ToString(), StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(numberOfVotes.ToString(), StringComparison.CurrentCultureIgnoreCase));
			Assert.IsTrue(result.Contains(score.ToString(), StringComparison.CurrentCultureIgnoreCase));
			movieServicesMock.Verify(msm => msm.CheckMovie(1), Times.Once);
		}
	}
}
