using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public String GenreType { get; set; }
        public ICollection<MovieGenre> movieGenres { get; set; }

    }
}
