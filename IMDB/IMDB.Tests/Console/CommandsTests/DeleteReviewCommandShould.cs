using IMDB.Console.Commands;
using IMDB.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMDB.Tests.Console.CommandsTests
{
    [TestClass]
    public class DeleteReviewCommandShould
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParametersAreNull()
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var deleteCommand = new DeleteReviewCommand(reviewService.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => deleteCommand.Run(null));
        }

        [DataTestMethod]
        [DataRow("More than one parameters")]        
        public void ReturnFailedSyntaxMessage_WhenParametersCountAreInvalid(string parameters)
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var deleteCommand = new DeleteReviewCommand(reviewService.Object);
            var args = parameters.Split(' ').ToList();
            const string FAILED_SYNTAX = "Wrong syntax of command";
            const string CMD_FORMAT = "deletereview - <reviewID>";

            // Act
            var result = deleteCommand.Run(args);

            // Assert
            Assert.IsTrue(result.Contains(FAILED_SYNTAX));
            Assert.IsTrue(result.Contains(CMD_FORMAT));
        }

        [DataTestMethod]
        [DataRow("3.14")]
        public void ReturnFailedSyntaxMessage_WhenParameterIsInvalid(string parameters)
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var deleteCommand = new DeleteReviewCommand(reviewService.Object);
            var args = parameters.Split(' ').ToList();
            const string FAILED_SYNTAX = "Wrong syntax of command";
            const string CMD_FORMAT = "deletereview - <reviewID>";

            // Act
            var result = deleteCommand.Run(args);

            // Assert
            Assert.IsTrue(result.Contains(CMD_FORMAT));
            Assert.IsTrue(result.Contains(FAILED_SYNTAX));
        }

        [DataTestMethod]
        [DataRow("5")]
        public void ReturnSuccessfulyMessage_WhenParameterIsCorrect(string parameters)
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var deleteCommand = new DeleteReviewCommand(reviewService.Object);
            var args = parameters.Split(' ').ToList();

            // Act
            var result = deleteCommand.Run(args);

            // Assert
            Assert.IsTrue(result.Contains($"{args[0]} deleted successfully"));
        }

        [DataTestMethod]
        [DataRow("5")]
        public void HitDeleteReview_WhenParameterIsCorrect(string parameters)
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var deleteCommand = new DeleteReviewCommand(reviewService.Object);
            var args = parameters.Split(' ').ToList();
            const int id = 5;

            // Act
            var result = deleteCommand.Run(args);

            // Assert
            reviewService.Verify(r => r.DeleteReview(id), Times.Once);
        }
    }
}
