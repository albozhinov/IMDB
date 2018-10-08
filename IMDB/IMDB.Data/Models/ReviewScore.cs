using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class ReviewScore
    {
        public int UserID { get; set; }
        public int ReviewID { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Review Review { get; set; }

    }
}
