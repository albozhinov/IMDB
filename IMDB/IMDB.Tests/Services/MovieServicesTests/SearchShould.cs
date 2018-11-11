using IMDB.Data.Models;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Tests.Services.MovieServicesTests
{
    [TestClass]
    public class SearchShould
    {
        const int numberOfVotes1Movie = 21;
        const int numberOfVotes1Review = 123;
        const int numberOfVotes2Review = 63;
        const int movieRating1 = 5;
        const int movieRating2 = 7;

        const string reviewText1 = "Cool";
        const string reviewText2 = "That movie sucks!";

        static MovieGenre movieGenre1 = new MovieGenre { Genre = new Genre { GenreType = "Action" } };
        static MovieGenre movieGenre2 = new MovieGenre { Genre = new Genre { GenreType = "porn" } };
        static MovieGenre movieGenre3 = new MovieGenre { Genre = new Genre { GenreType = "BDSM" } };
        static MovieGenre movieGenre4 = new MovieGenre { Genre = new Genre { GenreType = "lesbian" } };

        static User Pesho = new User { UserName = "pesho" };
        static User Gosho = new User { UserName = "gosho" };
        static User Sasho = new User { UserName = "sasho" };
        static User Tosho = new User { UserName = "tosho" };

        static ReviewRatings reviewRating1 = new ReviewRatings { ReviewRating = 1, User = Pesho };
        static ReviewRatings reviewRating2 = new ReviewRatings { ReviewRating = 4, User = Gosho };
        static ReviewRatings reviewRating3 = new ReviewRatings { ReviewRating = 5, User = Sasho };
        static ReviewRatings reviewRating4 = new ReviewRatings { ReviewRating = 2, User = Tosho };

        Review review1 = new Review
        {
            IsDeleted = false,
            NumberOfVotes = numberOfVotes1Review,
            MovieRating = movieRating1,
            Text = reviewText1,
            ReviewRatings = new List<ReviewRatings> { reviewRating1, reviewRating2 },
            ReviewScore = 2.5 //(1 + 4) / 2
        };
        Review review2 = new Review
        {
            IsDeleted = false,
            NumberOfVotes = numberOfVotes2Review,
            MovieRating = movieRating2,
            Text = reviewText2,
            ReviewRatings = new List<ReviewRatings> { reviewRating3, reviewRating4 },
            ReviewScore = 3.5 //(5 + 2) / 2
        };
        Movie Venom = new Movie
        {
            ID = 1,
            Name = "Venom",
            Director = new Director { Name = "thebsetdirector" },
            IsDeleted = false,
            NumberOfVotes = numberOfVotes1Movie,
            MovieGenres = new List<MovieGenre> { movieGenre2, movieGenre4 },
            MovieScore = 6 // (5 + 7) / 2 
        };
        Movie TheMovie23 = new Movie
        {
            ID = 2,
            Name = "23",
            Director = new Director { Name = "thebsetdirector" },
            IsDeleted = false,
            NumberOfVotes = numberOfVotes1Movie,
            MovieGenres = new List<MovieGenre> { movieGenre2, movieGenre4 },
            MovieScore = 6
        };

        [TestMethod]
        public async Task SearchAndFindMovie_WhenParametersAreCorrect()
        {
            //Arrange
            var expectedMovie = new List<Movie>();
            expectedMovie.Add(Venom);

            var reviewRepoMock = new Mock<IRepository<Review>>();
            var directorRepoMock = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            movieRepoMock
                .Setup(mr => mr.All())
                .Returns(new List<Movie> { Venom, TheMovie23 }.AsQueryable().BuildMock().Object);

            var sut = new MovieServices(reviewRepoMock.Object,
                movieRepoMock.Object, directorRepoMock.Object,
                genreRepoStub.Object, movieGenreRepoStub.Object);

            //Act
            var result = await sut.SearchMovieAsync("Venom", new List<string> { "porn" }, "thebsetdirector");
            //Assert
            Assert.AreEqual("Venom", result.FirstOrDefault().Name);
        }

        [TestMethod]
        public async Task ReturnsEmptyList_WhenMovieNameDoNotMach()
        {
            var reviewRepoMock = new Mock<IRepository<Review>>();
            var directorRepoMock = new Mock<IRepository<Director>>();
            var genreRepoStub = new Mock<IRepository<Genre>>();
            var movieGenreRepoStub = new Mock<IRepository<MovieGenre>>();

            var movieRepoMock = new Mock<IRepository<Movie>>();
            movieRepoMock
                .Setup(mr => mr.All())
                .Returns(new List<Movie> { Venom, TheMovie23 }.AsQueryable().BuildMock().Object);

            var sut = new MovieServices(reviewRepoMock.Object,
                movieRepoMock.Object, directorRepoMock.Object,
                genreRepoStub.Object, movieGenreRepoStub.Object);

            //Act
            var result = await sut.SearchMovieAsync("GoshoPansa", new List<string> { "NemaTakivJanr" }, "Bay Pesho");
			//Assert
			Assert.IsTrue(result.Count == 0);
        }
    }
}