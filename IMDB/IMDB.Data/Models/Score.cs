using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class Score
    {
        public int MovieID { get; set; }
        public int UserID { get; set; }
        public int ScoreValue { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }

    }
}
