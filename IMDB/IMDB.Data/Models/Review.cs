using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int MovieID { get; set; }
        public int UserID { get; set; }
		public bool IsDeleted { get; set; }
        public double MovieRating { get; set; }
        public double ReviewScore { get; set; }
        public String Text { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
        public ICollection<ReviewRatings> ReviewRatings { get; set; }
    }
}
