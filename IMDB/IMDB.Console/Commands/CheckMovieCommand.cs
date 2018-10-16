using System;
using System.Collections.Generic;
using System.Text;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
	public sealed class CheckMovieCommand : ICommand
	{
		private IMovieServices movieServices;
		private const string FAILED_SYNTAX = "Wrong syntax of command";
		private const string CMD_FORMAT = "checkmovie - <movieID>";
		public CheckMovieCommand(IMovieServices movieServices)
		{
			this.movieServices = movieServices;
		}
		public string Run(IList<string> parameters)
		{
			Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

			if (parameters.Count != 1)
				return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";

			int movieID;
			try
			{
				movieID = Int32.Parse(parameters[0]);
			}
			catch(Exception)
			{
				return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
			}
			var movieView = movieServices.CheckMovie(movieID);

			return movieView.ToString();
		}
	}
}
