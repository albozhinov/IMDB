using System;
using System.Collections.Generic;
using System.Linq;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
    public sealed class CreateMovieCommand : ICommand
    {
        private IMovieServices movieServices;
		private const string FAILED_SYNTAX = "Wrong syntax of command";
		private const string CMD_FORMAT = "createmovie - <movie name> : <movProducer> : <movG> : <movG>...";
        public CreateMovieCommand(IMovieServices movieServices)
		{
            this.movieServices = movieServices;
        }
        public string Run(IList<string> parameters)
        {
			Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

			if (parameters.Count < 3)
				return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";

			var movieName = parameters[0];
            var movieProducer = parameters[1];
            var movieGenres = parameters.Skip(2).ToList();

            movieServices.CreateMovie(movieName, movieGenres, movieProducer);

            return "Movie created successfully";
        }
    }
}
