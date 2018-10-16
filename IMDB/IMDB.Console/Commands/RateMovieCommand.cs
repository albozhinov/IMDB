using System;
using System.Collections.Generic;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
    public sealed class RateMovieCommand : ICommand
    {
        private IMovieServices movieServices;
        private const string FAILED_SYNTAX = "Wrong syntax of command";
        private const string CMD_FORMAT = "ratemovie - <movieID> : <rating> : <optional: text>";
        public RateMovieCommand(IMovieServices movieServices)
        {
            this.movieServices = movieServices;
        }
        public string Run(IList<string> parameters)
        {
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            if (parameters.Count < 2 || parameters.Count > 3)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }

            int movieID;
            double rating;
            string text = null;

            try
            {
                movieID = Int32.Parse(parameters[0]);
                rating = Double.Parse(parameters[1]);
                if (parameters.Count == 3)
                    text = parameters[2];

            }
            catch (Exception)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }


            movieServices.RateMovie(movieID, rating, text);

            return $"Your successfully created or updated a review for movie with ID: {movieID}";
        }
    }
}
