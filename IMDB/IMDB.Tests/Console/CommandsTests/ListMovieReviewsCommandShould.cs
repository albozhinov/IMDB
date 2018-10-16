using IMDB.Console.Commands;
using IMDB.Data.Views;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass]
    public class ListMovieReviewsCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParametersAreNull()
        {
            // Arrange
            var reviewServices = new Mock<IReviewsServices>();
            var listMovieReviewCommand = new ListMovieReviewsCommand(reviewServices.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => listMovieReviewCommand.Run(null));
        }

        [TestMethod]
        public void ReturnFailedSyntaxMessage_WhenParametersCountAreInvalid()
        {
            // Arrange
            var reviewServices = new Mock<IReviewsServices>();
            var listMovieReviewCommand = new ListMovieReviewsCommand(reviewServices.Object);
            var parameters = new List<string>() { "More", "than", "one", "parameter" };
            const string FAILED_SYNTAX = "Wrong syntax of command";
            const string CMD_FORMAT = "listmoviereviews - <movieID>";

            // Act 
            var result = listMovieReviewCommand.Run(parameters);

            // Assert
            Assert.IsTrue(result.Contains(CMD_FORMAT));
            Assert.IsTrue(result.Contains(FAILED_SYNTAX));
        }

        [DataTestMethod]
        [DataRow("5.55")]
        [DataRow("NotANumber")]
        public void ReturnFailedSyntaxMessage_WhenParameterIsInvalid(string parameter)
        {
            // Arrange
            var reviewServices = new Mock<IReviewsServices>();
            var listMovieReviewCommand = new ListMovieReviewsCommand(reviewServices.Object);
            var parameters = new List<string>() { parameter };
            const string FAILED_SYNTAX = "Wrong syntax of command";
            const string CMD_FORMAT = "listmoviereviews - <movieID>";

            // Act 
            var result = listMovieReviewCommand.Run(parameters);

            // Assert
            Assert.IsTrue(result.Contains(CMD_FORMAT));
            Assert.IsTrue(result.Contains(FAILED_SYNTAX));
        }

        [TestMethod]
        public void CallsToStringOnReviewView_WhenParameterIsCorrect()
        {
            // Arrange
            var reviewServices = new Mock<IReviewsServices>();
            var listMovieReviewCommand = new ListMovieReviewsCommand(reviewServices.Object);
            const int Id = 1;
            var parameters = new List<string>() { "1" };

            var reviewViewMock = new Mock<ReviewView>();

            reviewServices.Setup(r => r.ListMovieReviews(Id)).Returns(new List<ReviewView>() { reviewViewMock.Object });

            // Act 
            var result = listMovieReviewCommand.Run(parameters);

            // Assert
            reviewViewMock.Verify(rvm => rvm.ToString(), Times.Once);
        }
        [TestMethod]
        public void CallsService_WhenParameterIsCorrect()
        {
            // Arrange
            var reviewServices = new Mock<IReviewsServices>();
            var listMovieReviewCommand = new ListMovieReviewsCommand(reviewServices.Object);
            const int Id = 1;
            var parameters = new List<string>() { "1" };

            var reviewViewMock = new Mock<ReviewView>();

            reviewServices.Setup(r => r.ListMovieReviews(Id)).Returns(new List<ReviewView>() { reviewViewMock.Object });

            // Act 
            var result = listMovieReviewCommand.Run(parameters);

            // Assert
            reviewServices.Verify(r => r.ListMovieReviews(Id), Times.Once);
        }
    }
}
