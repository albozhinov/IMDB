using IMDB.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Web.Models
{
	public class ReviewViewModel
	{
        public ReviewViewModel()
        {

        }
		public ReviewViewModel(Review review)
		{
			this.ReviewID = review.ID;
			this.Rating = review.MovieRating.ToString();
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
        [Required]
        [RegularExpression(@"^([1-9]|10)$", ErrorMessage = "The movie rating must be between 1 and 10")]
        [StringLength(2, ErrorMessage = "The movie rating must be between one and two symbols", MinimumLength = 1)]
        public string Rating { get; set; }
		public double Score { get; set; }
        [Required]
        [MaxLength(250)]
		public string Text { get; set; }
		public string User { get; set; }
		public string UserID { get; set; }
		public string MovieName { get; set; }
        public int MovieId { get; set; }
        public int NumberOfVotes { get; set; }
        public string CurrentController { get; set; }
        public string CurrentAction { get; set; }
    }
}
