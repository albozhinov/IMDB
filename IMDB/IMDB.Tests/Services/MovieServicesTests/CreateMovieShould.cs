using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Tests.Services.MovieServicesTests
{
    [TestClass]
    public class CreateMovieShould
    {
        [TestMethod]
        public void ThrowNotEnoughPermissionsException_WhenUserIsNotAuthorized()
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "butnotcmdcreatemovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionMock.Object);
            // Act & Assert
            Assert.ThrowsException<NotEnoughPermissionException>(() => sut.CreateMovie("pishki", new List<string>() { "pulen s pishki" }, "pishkoglav"));
        }
        [DataTestMethod]
        [DataRow(null, "valid")]
        [DataRow("valid", null)]
        public void ThrowArgumentException_WhenArgumentsAreNull(string name, string producer)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionMock.Object);
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.CreateMovie(name, new List<string>(), producer));
        }
        [DataTestMethod]
        [DataRow("l", "valid")]
        [DataRow("itsgonnabesoannoyingtyping50symbolssothislittleshitisinvalidlololololoololololollolololollolololololololololollolololololollolololollololololololololololollolololo", "valid")]
        public void ThrowArgumentException_WhenArgumentsAreIncorrect(string name, string producer)
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionMock.Object);
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CreateMovie(name, new List<string>(), producer));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenGenresAreNull()
        {
            // Arrange
            var reviewRepoStub = new Mock<IRepository<Review>>();
            var movieRepoMock = new Mock<IRepository<Movie>>();
            var directorRepoStub = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoStub.Object, genreRepoStub.Object, movieGenreRepoStub.Object, loginSessionMock.Object);
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sut.CreateMovie("pishki", null, "pishkoglav"));
        }
        [TestMethod]
        public void CreateMovie_WhenDirectorDoesNotExist()
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
            Movie movieCreatedBySut = null;
            movieRepoMock
                .Setup(mr => mr.Add(It.IsAny<Movie>()))
                .Callback<Movie>(m => movieCreatedBySut = m);

            var directorRepoMock = new Mock<IRepository<Director>>();
            var genreRepoMock = new Mock<IRepository<Genre>>();
            var genreList = new List<Genre>();
            genreList.Add(new Genre { GenreType = gentre1, ID = gentre1ID });
            genreList.Add(new Genre { GenreType = gentre2, ID = gentre2ID });
            genreList.Add(new Genre { GenreType = "randoms", ID = 21412343 });
            genreRepoMock
                .Setup(grm => grm.All())
                .Returns(genreList.AsQueryable());

            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();
            var movieGenresCreatedBySut = new List<MovieGenre>();
            movieGenreRepoMock
                .Setup(mgm => mgm.Add(It.IsAny<MovieGenre>()))
                .Callback<MovieGenre>(mg => movieGenresCreatedBySut.Add(mg));

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object, loginSessionMock.Object);
            // Act 
            sut.CreateMovie(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            Assert.IsTrue(movieCreatedBySut.Director.Name == directorName);
            Assert.IsTrue(movieCreatedBySut.Name == movieName);
            Assert.IsTrue(movieGenresCreatedBySut.Count == 2);
            Assert.IsTrue(movieGenresCreatedBySut.Any(mg => mg.GenreID == gentre1ID && mg.MovieID == movieCreatedBySut.ID));
            Assert.IsTrue(movieGenresCreatedBySut.Any(mg => mg.GenreID == gentre2ID && mg.MovieID == movieCreatedBySut.ID));
            movieRepoMock.Verify(mm => mm.Add(movieCreatedBySut), Times.Once);
            movieGenreRepoMock.Verify(mgm => mgm.Add(It.IsAny<MovieGenre>()), Times.Exactly(2));
        }
        [TestMethod]
        public void CreateMovie_WhenDirectorExistAndSuchMovieDoesNotExist()
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
            Movie movieCreatedBySut = null;
            movieRepoMock
                .Setup(mr => mr.Add(It.IsAny<Movie>()))
                .Callback<Movie>(m => movieCreatedBySut = m);

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }.AsQueryable());

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var genreList = new List<Genre>();
            genreList.Add(new Genre { GenreType = gentre1, ID = gentre1ID });
            genreList.Add(new Genre { GenreType = gentre2, ID = gentre2ID });
            genreList.Add(new Genre { GenreType = "randoms", ID = 21412343 });
            genreRepoMock
                .Setup(grm => grm.All())
                .Returns(genreList.AsQueryable());

            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();
            var movieGenresCreatedBySut = new List<MovieGenre>();
            movieGenreRepoMock
                .Setup(mgm => mgm.Add(It.IsAny<MovieGenre>()))
                .Callback<MovieGenre>(mg => movieGenresCreatedBySut.Add(mg));

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object, loginSessionMock.Object);
            // Act 
            sut.CreateMovie(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            Assert.IsTrue(movieCreatedBySut.Name == movieName);
            Assert.IsTrue(movieCreatedBySut.DirectorID == directorID);
            Assert.IsTrue(movieGenresCreatedBySut.Count == 2);
            Assert.IsTrue(movieGenresCreatedBySut.Any(mg => mg.GenreID == gentre1ID && mg.MovieID == movieCreatedBySut.ID));
            Assert.IsTrue(movieGenresCreatedBySut.Any(mg => mg.GenreID == gentre2ID && mg.MovieID == movieCreatedBySut.ID));
            movieRepoMock.Verify(mm => mm.Add(movieCreatedBySut), Times.Once);
            movieGenreRepoMock.Verify(mgm => mgm.Add(It.IsAny<MovieGenre>()), Times.Exactly(2));
        }
        [TestMethod]
        public void UpdateMovie_WhenSuchExists()
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
                .Returns(new List<Movie>() { movie }.AsQueryable());

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }.AsQueryable());

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object, loginSessionMock.Object);
            // Act 
            sut.CreateMovie(movieName, new List<string>() { gentre1, gentre2 }, directorName);
            //Assert
            movieRepoMock.Verify(mm => mm.Update(movie), Times.Once);
            Assert.IsTrue(!movie.IsDeleted);
        }
        [TestMethod]
        public void ThrowMovieExistsException_WhenMovieExists()
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
                .Returns(new List<Movie>() { movie }.AsQueryable());

            var directorRepoMock = new Mock<IRepository<Director>>();
            directorRepoMock
                .Setup(dr => dr.All())
                .Returns(new List<Director>() { new Director() { Name = directorName, ID = directorID } }.AsQueryable());

            var genreRepoMock = new Mock<IRepository<Genre>>();
            var movieGenreRepoMock = new Mock<IRepository<MovieGenre>>();

            var loginSessionMock = new Mock<ILoginSession>();
            loginSessionMock
                .SetupGet(ls => ls.LoggedUserPermissions)
                .Returns(new List<string>() { "cmd0", "cmd1", "createmovie" });

            var sut = new MovieServices(reviewRepoStub.Object, movieRepoMock.Object, directorRepoMock.Object, genreRepoMock.Object, movieGenreRepoMock.Object, loginSessionMock.Object);
            // Act & Assert
            Assert.ThrowsException<MovieExistsException>(() => sut.CreateMovie(movieName, new List<string>() { gentre1, gentre2 }, directorName));
        }
    }
}
