using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class ScoresUserMovie
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
