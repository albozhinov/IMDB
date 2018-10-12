using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.Commands
{
    public class RateReviewCommand : ICommand
    {
        private IReviewsServices reviewService;

        public RateReviewCommand(IReviewsServices reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Run(IList<string> parameters)
        {
            //int reviewID, double score
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

            return "";
        }
    }
}
