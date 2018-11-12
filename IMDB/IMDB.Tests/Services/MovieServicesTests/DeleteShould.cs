using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Tests.Services.MovieServicesTests
{
	[TestClass]
	public class DeleteShould
	{
		[DataTestMethod]
		[DataRow(0)]
		[DataRow(-1)]
		public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(int movieID)
		{
			// Arrange
			var reviewRepoStub = new Mock<IRepository<Review>>();

			var movieRepoMock = new Mock<IRepository<Movie>>();
			var directorRepoStub = new Mock<IRepository<Director>>();
			var genreRepoStub = new Mock<IRepository<Genre>>();

			var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

			var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
			// Act & Assert
			await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DeleteMovieAsync(movieID));
		}
		[TestMethod]
		public async Task ThrowsMovieNotFoundException_WhenMovieDoesNotExist()
		{
			// Arrange
			const int movieID = 1;

			var reviewRepoStub = new Mock<IRepository<Review>>();

			var movieRepoMock = new Mock<IRepository<Movie>>();
			movieRepoMock
				.Setup(mr => mr.All())
				.Returns(new List<Movie>() { new Movie { ID = movieID, IsDeleted = false } }
													.AsQueryable()
													.BuildMock()
													.Object);

			var directorRepoStub = new Mock<IRepository<Director>>();
			var genreRepoStub = new Mock<IRepository<Genre>>();
			var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

			var sut = new MovieServices(reviewRepoStub.Object,
				movieRepoMock.Object, directorRepoStub.Object,
				genreRepoStub.Object, movieGenreRepoStub.Object);
			// Act & Assert
			await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await sut.DeleteMovieAsync(12312));
		}
		[TestMethod]
		public async Task ThrowsMovieNotFoundException_WhenMovieIsDeleted()
		{
			// Arrange
			const int movieID = 1;

			var reviewRepoStub = new Mock<IRepository<Review>>();

			var movieRepoMock = new Mock<IRepository<Movie>>();
			movieRepoMock
				.Setup(mr => mr.All())
				.Returns(new List<Movie>() { new Movie { ID = movieID, IsDeleted = true } }
														.AsQueryable()
														.BuildMock()
														.Object);

			var directorRepoStub = new Mock<IRepository<Director>>();
			var genreRepoStub = new Mock<IRepository<Genre>>();
			var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

			var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
			// Act & Assert
			await Assert.ThrowsExceptionAsync<MovieNotFoundException>(async () => await sut.DeleteMovieAsync(movieID));
		}
		[TestMethod]
		public async Task DeletesAllMovieInformation_WhenArgumentsAreCorrect()
		{
			// Arrange
			const int movieID = 1;
			var reviewRepoStub = new Mock<IRepository<Review>>();

			var movieRepoMock = new Mock<IRepository<Movie>>();
			var review1 = new Review { IsDeleted = false };
			var review2 = new Review { IsDeleted = false };
			var review3 = new Review { IsDeleted = true };
			var movie = new Movie
			{
				ID = movieID,
				IsDeleted = false,
				Reviews = new List<Review>() { review1, review2, review3 }
			};
			movieRepoMock
				.Setup(mr => mr.All())
				.Returns(new List<Movie>() { movie }.AsQueryable().BuildMock().Object);

			var directorRepoStub = new Mock<IRepository<Director>>();
			var genreRepoStub = new Mock<IRepository<Genre>>();
			var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

			var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
			// Act
			await sut.DeleteMovieAsync(movieID);
			// Assert
			Assert.IsTrue(movie.IsDeleted);
			Assert.IsTrue(movie.Reviews.All(r => r.IsDeleted));
		}
	}
}
