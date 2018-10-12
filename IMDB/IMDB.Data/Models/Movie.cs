using System;
using System.Collections.Generic;

namespace IMDB.Data.Models
{
	public class Movie
	{
		public int ID { get; set; }
		public String Name { get; set; }
		public double MovieScore { get; set; }
		public bool IsDeleted { get; set; }
        public int ProducerID { get; set; }
        public Producer Producer { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
