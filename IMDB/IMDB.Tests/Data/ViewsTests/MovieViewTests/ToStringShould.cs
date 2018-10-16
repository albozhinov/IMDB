using IMDB.Data.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace IMDB.Tests.Data.ViewsTests.MovieViewTests
{
    [TestClass]
    public class ToStringShould
    {
        [TestMethod]
        public void CorrectlyReturnTheDataOfView_WhenCalled()
        {
            const string movieName = "someName";
            const string director = "director";
            const string genre1 = "g1";
            const string genre2 = "g2";
            const int ID = 0;
            const int numberOfVotes = 92;
            const int score = 12;
            var reviewViewMock1 = new Mock<ReviewView>();
            var reviewViewMock2 = new Mock<ReviewView>();
            //Arrange
            var sut = new MovieView
            {
                ID = ID,
                Name = movieName,
                Director = director,
                Genres = new List<string> { genre1, genre2 },
                NumberOfVotes = numberOfVotes,
                Score = score,
                Top5Reviews = new List<ReviewView> {reviewViewMock1.Object, reviewViewMock2.Object }
            };
            //Act
            var result = sut.ToString();
            //Assert
            Assert.IsTrue(result.Contains(movieName, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(director, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(genre1, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(genre2, StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(numberOfVotes.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(score.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.IsTrue(result.Contains(ID.ToString(), StringComparison.CurrentCultureIgnoreCase));
            reviewViewMock1.Verify(rvm => rvm.ToString(), Times.Once);
            reviewViewMock2.Verify(rvm => rvm.ToString(), Times.Once);
        }
    }
}
