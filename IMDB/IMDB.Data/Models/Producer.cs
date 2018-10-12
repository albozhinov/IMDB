using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Models
{
    public class Producer
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
