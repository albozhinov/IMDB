using System;
using System.Collections.Generic;
using System.Text;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
    public class SearchMovieCommand : ICommand
    {
        private IMovieServices movieServices;

        public SearchMovieCommand(IMovieServices movieServices)
        {
            this.movieServices = movieServices;
        }
        public string Run(IList<string> parameters)
        {
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            //if (parameters.Count != 3)
            //return $"Wrong search parameters\n";
            string movieName, movieProducer, movieGenre;

            movieName = parameters[0];
            movieGenre = parameters[1];
            movieProducer = parameters[2];

            if (string.IsNullOrEmpty(movieName) || string.IsNullOrWhiteSpace(movieName))
            {
                movieName = null;
            }
            if (string.IsNullOrEmpty(movieGenre) || string.IsNullOrWhiteSpace(movieGenre))
            {
                movieGenre = null;
            }
            if (string.IsNullOrEmpty(movieProducer) || string.IsNullOrWhiteSpace(movieProducer))
            {
                movieProducer = null;
            }
            var movieView = movieServices.SearchMovie(movieName, movieGenre, movieProducer);
            var sb = new StringBuilder();

            if (movieView.Count > 0)
            {
                sb.AppendLine("Finded movies:");
                foreach (var item in movieView)
                {
                    sb.Append(item.ToString().Remove(item.ToString().IndexOf("Top")));
                }
            }
            else sb.AppendLine("No movies founded! \nProbably the movie doesn't exit in database\nOR\nthe input parameters are wrong!");


            return sb.ToString();
        }
    }
}
