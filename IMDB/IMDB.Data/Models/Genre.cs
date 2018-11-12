using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.Models
{
    public class Genre
    {
        public int ID { get; set; }
        [Required]
        public String GenreType { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
