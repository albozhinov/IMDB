using IMDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Web.Models
{
    public class ReviewRateViewModel
    {      
        public int ReviewId { get; set; }

        public double ReviewRating { get; set; }

        public User User { get; set; }
    }
}
