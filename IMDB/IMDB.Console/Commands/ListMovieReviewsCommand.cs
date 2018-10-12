using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IMDB.Console.Commands
{
    public class ListMovieReviewsCommand : ICommand
    {
        private IReviewsServices reviewService;

        public ListMovieReviewsCommand(IReviewsServices reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Run(IList<string> parameters)
        {
            bool isParse = int.TryParse(parameters[0], out int ID);

            if (!isParse)
            {
                return "Incorrect ID format";
            }

            var reviews = reviewService.ShowReviews(ID);
            var first = reviews.FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine($"Movie: {first.MovieName}");

            foreach (var review in reviews)
            {
                sb.AppendLine($"# |Review rating: {review.Rating}| |Review score: {review.Score}| |User: {review.ByUser}|");
                sb.AppendLine($"---{review.Text}");
            }

            return sb.ToString();
        }
    }
}
