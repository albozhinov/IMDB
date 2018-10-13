using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.Commands
{
    public sealed class RateReviewCommand : ICommand
    {
        private IReviewsServices reviewService;
        private const string FAILED_SYNTAX = "Wrong syntax of command";
        private const string CMD_FORMAT = "ratereview - <review> : <rating>";
        public RateReviewCommand(IReviewsServices reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Run(IList<string> parameters)
        {            
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            if (parameters.Count != 2)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }

            var isParseID = int.TryParse(parameters[0], out int reviewID);
            var isParseRating = double.TryParse(parameters[1], out double rating);

            if (!isParseID || !isParseRating)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }

            var rateReview = reviewService.RateReview(reviewID, rating);
            
            var sb = new StringBuilder();

            sb.AppendLine($"The rated review for movie \"{rateReview.MovieName}\":");
            sb.Append(rateReview.ToString());
            return sb.ToString();
        }
    }
}
