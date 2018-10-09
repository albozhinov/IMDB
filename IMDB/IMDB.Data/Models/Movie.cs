using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
	public class Movie
	{
		public int ID { get; set; }
		public String Name { get; set; }
		public double Score { get; set; }
		public String Genre { get; set; }
		public String Producer { get; set; }
		public bool IsDeleted { get; set; }
		public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieGenre> movieGenres { get; set; }
    }
}
