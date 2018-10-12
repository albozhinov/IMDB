using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IMDB.Data.Models
{
    public class Director
    {
        public int ID { get; set; }
        [Required]
        [MaxLength (78)]
        public String Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
