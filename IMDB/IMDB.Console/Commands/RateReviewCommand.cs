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

        public RateReviewCommand(IReviewsServices reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Run(IList<string> parameters)
        {            
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            if (parameters.Count != 2)
            {
                return "Invalid count of parameters.";
            }

            var isParseID = int.TryParse(parameters[0], out int reviewID);
            var isParseScore = double.TryParse(parameters[1], out double score);

            if (!isParseID || !isParseScore)
            {
                return "Incorrect parameters format.";
            }

            var rateReview = reviewService.RateReview(reviewID, score);
            
            var sb = new StringBuilder();

            sb.AppendLine($"Movie: {rateReview.MovieName}");
            sb.Append(rateReview.ToString());
            return sb.ToString();
        }
    }
}
