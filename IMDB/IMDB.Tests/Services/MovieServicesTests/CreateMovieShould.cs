using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
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
    public class CreateMovieShould
    {
        [DataTestMethod]
        [DataRow(null, "valid")]
        [DataRow("valid", null)]
        public async Task ThrowArgumentException_WhenArgumentsAreNull(string name, string producer)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.CreateMovieAsync(name, new List<string>(), producer));
        }
        [DataTestMethod]
        [DataRow("l", "valid")]
        [DataRow("itsgonnabesoannoyingtyping50symbolssothislittleshitisinvalidlololololoololololollolololollolololololololololollolololololollolololollololololololololololollolololo", "valid")]
        public async Task ThrowArgumentException_WhenArgumentsAreIncorrect(string name, string producer)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.CreateMovieAsync(name, new List<string>(), producer));
        }
        [TestMethod]
        public async Task ThrowArgumentException_WhenGenresAreNull()
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateMovieAsync("pishki", null, "pishkoglav"));
        }
        [TestMethod]
        public async Task CreateMovie_WhenDirectorDoesNotExist()
        {
            // Arrange
            string directorName = "d1";
            const string gentre1 = "g1";
            const int gentre1ID = 12;
            const string gentre2 = "g2";
            const int gentre2ID = 21;
            const string movieName = "movieName";
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();

            var directorRepoMock = new Mock<IRepository<Director>>();
			directorRepoMock
				.Setup(dr => dr.All())
				.Returns(new List<Director>().AsQueryable().BuildMock().Object);
			var genreRepoMock = new Mock<IRepository<Genre>>();
            var genreList = new List<Genre>();
            genreList.Add(new Genre { GenreType = gentre1, ID = gentre1ID });
            genreList.Add(new Genre { GenreType = gentre2, ID = gentre2ID });
            genreList.Add(new Genre { GenreType = "randoms", ID = 21412343 });
            genreRepoMock
                .Setup(grm => grm.All())
                .Returns(genreList.AsQueryable().BuildMock().Object);

            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object);
            // Act 
            var movieCreatedBySut = await sut.CreateMovieAsync(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            Assert.IsTrue(movieCreatedBySut.Director.Name == directorName);
            Assert.IsTrue(movieCreatedBySut.Name == movieName);
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Count == 2);
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Any(mg => mg.GenreID == gentre1ID && mg.MovieID == movieCreatedBySut.ID));
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Any(mg => mg.GenreID == gentre2ID && mg.MovieID == movieCreatedBySut.ID));
            movieRepoMock.Verify(mm => mm.AddAsync(movieCreatedBySut), Times.Once);
            movieRepoMock.Verify(mm => mm.SaveAsync(), Times.Once);
		}
        [TestMethod]
        public async Task CreateMovie_WhenDirectorExistAndSuchMovieDoesNotExist()
        {
            // Arrange
            string directorName = "d1";
            const int directorID = 23;
            const string gentre1 = "g1";
            const int gentre1ID = 12;
            const string gentre2 = "g2";
            const int gentre2ID = 21;
            const string movieName = "movieName";
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
			movieRepoMock
				.Setup(mr => mr.All())
				.Returns(new List<Movie>().AsQueryable().BuildMock().Object);

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }
																.AsQueryable()
																.BuildMock()
																.Object);

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var genreList = new List<Genre>();
            genreList.Add(new Genre { GenreType = gentre1, ID = gentre1ID });
            genreList.Add(new Genre { GenreType = gentre2, ID = gentre2ID });
            genreList.Add(new Genre { GenreType = "randoms", ID = 21412343 });
            genreRepoMock
                .Setup(grm => grm.All())
                .Returns(genreList.AsQueryable().BuildMock().Object);

            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object);
            // Act 
            var movieCreatedBySut = await sut.CreateMovieAsync(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            Assert.IsTrue(movieCreatedBySut.Name == movieName);
            Assert.IsTrue(movieCreatedBySut.DirectorID == directorID);
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Count == 2);
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Any(mg => mg.GenreID == gentre1ID && mg.MovieID == movieCreatedBySut.ID));
            Assert.IsTrue(movieCreatedBySut.MovieGenres.Any(mg => mg.GenreID == gentre2ID && mg.MovieID == movieCreatedBySut.ID));
            movieRepoMock.Verify(mm => mm.AddAsync(movieCreatedBySut), Times.Once);
            movieRepoMock.Verify(mm => mm.SaveAsync(), Times.Once);
		}
		[TestMethod]
        public async Task UpdateMovie_WhenSuchExists()
        {
            // Arrange
            string directorName = "d1";
            const int directorID = 23;
            const string gentre1 = "g1";
            const string gentre2 = "g2";
            const string movieName = "movieName";
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movie = new Movie { Name = movieName, Director = new Director { Name = directorName, ID = directorID }, IsDeleted = true };
            movieRepoMock
                .Setup(mrm => mrm.All())
                .Returns(new List<Movie>() { movie }.AsQueryable().BuildMock().Object);

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }
																.AsQueryable()
																.BuildMock()
																.Object);

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object);
            // Act 
            await sut.CreateMovieAsync(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            movieRepoMock.Verify(mm => mm.Update(movie), Times.Once);
            Assert.IsTrue(!movie.IsDeleted);
        }
        [TestMethod]
        public async Task ThrowMovieExistsException_WhenMovieExists()
        {
            // Arrange
            string directorName = "d1";
            const int directorID = 23;
            const string gentre1 = "g1";
            const string gentre2 = "g2";
            const string movieName = "movieName";
            var reviewRepoStub = new Mock<IRepository<Review>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            var movie = new Movie { Name = movieName, Director = new Director { Name = directorName, ID = directorID }, IsDeleted = false };
            movieRepoMock
                .Setup(mrm => mrm.All())
                .Returns(new List<Movie>() { movie }.AsQueryable().BuildMock().Object);

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }
																.AsQueryable()
																.BuildMock()
																.Object);

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object);
            // Act & Assert
            await Assert.ThrowsExceptionAsync<MovieExistsException>(async () => await sut.CreateMovieAsync(movieName, new List<string>() { gentre1, gentre2 }, directorName));
        }
    }
}
