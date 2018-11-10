using IMDB.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IMDB.Web.Models
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {

        }
        public MovieViewModel(Movie movie)
        {
            this.ID = movie.ID;
            this.Name = movie.Name;
            this.Score = movie.MovieScore;
            this.Director = movie.Director.Name;
            this.Genres = movie
                .MovieGenres
                .Select(x => x.Genre.GenreType)
                .ToList();
            this.Top5Reviews = movie
                .Reviews?
                .Where(r => !r.IsDeleted)
                .Take(5)
                .OrderByDescending(r => r.ReviewScore)
                .Select(r => new ReviewViewModel(r))
                .ToList();
            if (movie.Reviews?.Count > 5)
            {
                this.More5Reviews = true;
            }
            else
            {
                this.More5Reviews = false;
            }
            this.NumberOfVotes = movie.NumberOfVotes;
        }
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The movie name must be between 3 and 50 symbols", MinimumLength = 4)]
        public string Name { get; set; }
        public double Score { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "The movie name must be between 3 and 25 symbols", MinimumLength = 4)]
        public string Director { get; set; }
        [Required]
        public ICollection<string> Genres { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
        public ICollection<ReviewViewModel> Top5Reviews { get; set; }
        public bool More5Reviews { get; set; }
        public int NumberOfVotes { get; set; }
    }
}
