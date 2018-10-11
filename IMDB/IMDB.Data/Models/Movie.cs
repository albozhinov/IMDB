using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
	public class Movie
	{
		public int ID { get; set; }
		public int MovieGenreID { get; set; }
		public String Name { get; set; }
		public double MovieScore { get; set; }
		public String Producer { get; set; }
		public bool IsDeleted { get; set; }
		public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
