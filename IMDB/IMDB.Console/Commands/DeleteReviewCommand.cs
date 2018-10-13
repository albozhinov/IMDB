using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.Commands
{
    public sealed class DeleteReviewCommand : ICommand
    {
        private IReviewsServices reviewService;
        private const string FAILED_SYNTAX = "Wrong syntax of command";
        private const string CMD_FORMAT = "deletereview - <reviewID>";

        public DeleteReviewCommand(IReviewsServices reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Run(IList<string> parameters)
        {
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            if (parameters.Count != 1)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }

            bool isParse = int.TryParse(parameters[0], out int ID);

            if (!isParse)
            {
                return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
            }

            reviewService.DeleteReview(ID);

            return $"Review with ID: {ID} deleted successfully";
        }
    }
}
