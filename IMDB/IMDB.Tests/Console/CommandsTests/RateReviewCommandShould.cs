using IMDB.Console.Commands;
using IMDB.Data.Views;
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
    public class RateReviewCommandShould
    {
        [DataTestMethod]
        [DataRow("Less")]
        [DataRow("Invalid params")]
        [DataRow("1 9.00 More than two")]

        public void ReturnFailedSyntaxMessage_WhenParametersAreInvalid(string args)
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var rateReviewCommand = new RateReviewCommand(reviewService.Object);
            var parameters = args.Split().ToList();
            const string FAILED_SYNTAX = "Wrong syntax of command";
            const string CMD_FORMAT = "ratereview - <review> : <rating>";

            // Act
            var result = rateReviewCommand.Run(parameters);

            // Assert
            Assert.IsTrue(result.Contains(FAILED_SYNTAX));
            Assert.IsTrue(result.Contains(CMD_FORMAT));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenParametersAreNull()
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var rateReviewCommand = new RateReviewCommand(reviewService.Object);

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => rateReviewCommand.Run(null));
        }

        [TestMethod]
        public void ReturnSuccess_WhenParametersAreCorrect()
        {
            // Arrange
            var reviewService = new Mock<IReviewsServices>();
            var rateReviewCommand = new RateReviewCommand(reviewService.Object);
            var parameters = new List<string>() { "1", "9.00" };
            const string successMessage = "The rated review for movie";
            const int reviewId = 1;
            const double rating = 9.00;
            var reviewViewMock = new Mock<ReviewView>();
            reviewService.Setup(r => r.RateReview(reviewId, rating)).Returns(reviewViewMock.Object);

            // Act 
            var result = rateReviewCommand.Run(parameters);

            // Assert 
            Assert.IsTrue(result.Contains(successMessage));
            reviewViewMock.Verify(rvm => rvm.ToString(), Times.Once);
            reviewService.Verify(r => r.RateReview(reviewId, rating), Times.Once);
        }

    }
}
