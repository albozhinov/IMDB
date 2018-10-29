using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.Models
{
    public class Review
    {
        public int ID { get; set; }
        [Required]
        public int MovieID { get; set; }
        [Required]
        public string UserID { get; set; }
		public bool IsDeleted { get; set; }
        public double MovieRating { get; set; }
        public double ReviewScore { get; set; }
		public int NumberOfVotes { get; set; }
        [Required]
        [MaxLength(250)]
        public String Text { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
        public ICollection<ReviewRatings> ReviewRatings { get; set; }
    }
}
