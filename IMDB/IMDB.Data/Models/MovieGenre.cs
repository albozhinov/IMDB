using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class MovieGenre
    {
        public int MovieID { get; set; }
        public int GenreID { get; set; }
        public Movie movie { get; set; }
        public Genre genre { get; set; }
    }
}
