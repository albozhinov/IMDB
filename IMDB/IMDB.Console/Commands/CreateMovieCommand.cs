using System.Collections.Generic;
using System.Linq;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;

namespace IMDB.Console.Commands
{
    public sealed class CreateMovieCommand : ICommand
    {
        private IMovieServices movieServices;
        public CreateMovieCommand(IMovieServices movieServices)
        {
            this.movieServices = movieServices;
        }
        public string Run(IList<string> parameters)
        {
            //Validate. If something is wrong, print the help command for movies
            var movieName = parameters[0];
            var movieProducer = parameters[1];
            var movieGenres = parameters.Skip(2).ToList();

            movieServices.CreateMovie(movieName, movieGenres, movieProducer);

            return "Movie created successfully";
        }
    }
}
