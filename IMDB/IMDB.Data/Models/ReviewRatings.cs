namespace IMDB.Data.Models
{
    public class ReviewRatings
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int ReviewId { get; set; }
        public double ReviewRating { get; set; }
        public User User { get; set; }
        public Review Review { get; set; }
    }
}
