using System;
using System.Collections.Generic;
using System.Linq;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;

namespace IMDB.Console.Commands
{
	public sealed class CheckMovieCommand : ICommand
	{
		private IMovieServices movieServices;
		public CheckMovieCommand(IMovieServices movieServices)
		{
			this.movieServices = movieServices;
		}
		public string Run(IList<string> parameters)
		{
			//Validate. If something is wrong, print the help command for movies
			var movieID = Int32.Parse(parameters[0]);

			movieServices.Check(movieID);

			return "Movie created successfully";
		}
	}
}
