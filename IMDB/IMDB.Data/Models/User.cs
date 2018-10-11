using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
		public string Password { get; set; }
        public String Role { get; set; }
        public int Rank { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewRatings> ReviewRatings { get; set; }
    }
}
