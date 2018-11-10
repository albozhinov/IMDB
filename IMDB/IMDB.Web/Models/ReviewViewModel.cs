using IMDB.Data.Models;

namespace IMDB.Web.Models
{
	public class ReviewViewModel
	{
		public ReviewViewModel(Review review)
		{
			this.ReviewID = review.ID;
			this.Rating = review.MovieRating;
			this.Score = review.ReviewScore;
			this.Text = review.Text;
			this.User = review.User.Email;
			this.UserID = review.User.Id;
			this.MovieName = review.Movie.Name;
            this.MovieId = review.MovieID;
			this.NumberOfVotes = review.NumberOfVotes;
            this.User = review.User.UserName;
		}
		public int ReviewID { get; set; }
		public double Rating { get; set; }
		public double Score { get; set; }
		public string Text { get; set; }
		public string User { get; set; }
		public string UserID { get; set; }
		public string MovieName { get; set; }
        public int MovieId { get; set; }
        public int NumberOfVotes { get; set; }
	}
}
