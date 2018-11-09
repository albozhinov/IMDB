using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.Models
{
	public class Movie
	{
		public int ID { get; set; }        
		public String Name { get; set; }
		public double MovieScore { get; set; }
		public int NumberOfVotes { get; set; }
		public bool IsDeleted { get; set; }
		public int DirectorID { get; set; }
        public Director Director { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
