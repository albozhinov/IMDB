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
        public double Score { get; set; }
        public String Text { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
        public ICollection<ReviewScore> ReviewScores { get; set; }
    }
}
