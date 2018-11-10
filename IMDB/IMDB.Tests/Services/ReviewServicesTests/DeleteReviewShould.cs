using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;

namespace IMDB.Tests.Services.ReviewServicesTests
{
	[TestClass]
	public class DeleteReviewShould
	{
		[DataTestMethod]
		[DataRow(null)]
		[DataRow(0)]
		public async Task ThrowArgumentException_WhenParametersAreIncorrect(int reviewId)
		{
			// Arrange
			var movieRepoStub = new Mock<IRepository<Movie>>();
			var reviewRepoMock = new Mock<IRepository<Review>>();
			var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

			var reviewMock = new Review()
			{
				ID = 1,
				IsDeleted = false,
				Text = "Mnogo qk Unit Test!"
			};

			var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
			reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

			var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

			// Act and Assert
			await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await reviewServices.DeleteReviewAsync(reviewId));
		}

		[DataTestMethod]
		[DataRow(1, true)]
		[DataRow(5, false)]
		public async Task ThrowReviewNotFoundException_WhenReviewNotFound(int reviewId, bool flag)
		{
			// Arrange
			var movieRepoStub = new Mock<IRepository<Movie>>();
			var reviewRepoMock = new Mock<IRepository<Review>>();
			var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

			var reviewMock = new Review()
			{
				ID = 1,
				IsDeleted = flag,
				Text = "Mnogo qk Unit Test!"
			};

			var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
			reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

			var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);

			// Act and Assert
			await Assert.ThrowsExceptionAsync<ReviewNotFoundException>(async () => await reviewServices.DeleteReviewAsync(reviewId));
		}

		[TestMethod]
		public async Task DeletedReview_WhenUserHasPermission()
		{
			// Arrange
			const int reviewId = 1;
			const bool deletedFlag = false;
			var movieRepoStub = new Mock<IRepository<Movie>>();
			var reviewRepoMock = new Mock<IRepository<Review>>();
			var reviewRatingsStub = new Mock<IRepository<ReviewRatings>>();

			var movie = new Movie()
			{
				Name = "American Pie",
				ID = 1,
				NumberOfVotes = 100,
			};

			var reviewMock = new Review()
			{
				ID = 1,
				IsDeleted = deletedFlag,
				Text = "Mnogo qk Unit Test!",
				Movie = movie
			};

			var allReviews = new List<Review>() { reviewMock }.AsQueryable().BuildMock().Object;
			reviewRepoMock.Setup(m => m.All()).Returns(allReviews);

			var reviewServices = new ReviewsService(reviewRepoMock.Object, movieRepoStub.Object, reviewRatingsStub.Object);
			// Act
			await reviewServices.DeleteReviewAsync(reviewId);

			// Assert
			Assert.IsTrue(reviewMock.IsDeleted == true);
			Assert.IsTrue(reviewMock.Movie.NumberOfVotes == 99);
			reviewRepoMock.Verify(rRepo => rRepo.Update(reviewMock), Times.Once);
			reviewRepoMock.Verify(rRepo => rRepo.SaveAsync(), Times.Once);
		}
	}
}
