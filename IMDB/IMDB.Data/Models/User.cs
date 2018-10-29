using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace IMDB.Data.Models
{
    public class User : IdentityUser
	{
        public int Rank { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewRatings> ReviewRatings { get; set; }
    }
}
