using IMDB.Data.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Data.ViewsTests.ReviewViewTests
{
    [TestClass]
    public class ToStringShould
    {
        [TestMethod]
        public void CorrectlyReturnTheDataOfView_WhenCalled()
        {
            const string username = "someName";
            const int numberOfVotes = 92;
            const int score = 12;
            const int reviewID = 8;
            const double rating = 2.3;
            const string text = "the movie is the best lolz";
            //Arrange
            var sut = new ReviewView 
            {
                ByUser = username,
                NumberOfVotes = numberOfVotes,
                Rating = rating,
                ReviewID = reviewID,
                Score = score,
                Text = text
            };
            //Act
            var result = sut.ToString();
            //Assert
            Assert.IsTrue(result.Contains(username, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(text, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(numberOfVotes.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(score.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(reviewID.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(rating.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
