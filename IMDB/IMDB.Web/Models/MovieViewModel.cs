﻿using IMDB.Data.Models;
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
			this.Top6Reviews = movie
				.Reviews
                .Where(r => !r.IsDeleted)
                .Take(6)
				.OrderByDescending(r => r.ReviewScore)
				.Select(r => new ReviewViewModel(r))
				.ToList();
            this.NumberOfVotes = movie.NumberOfVotes;
		}        
        public int ID { get; set; }
		public string Name { get; set; }
		public double Score { get; set; }
		public string Director { get; set; }
		public ICollection<string> Genres { get; set; }
		public IEnumerable<SelectListItem> GenreList { get; set; }
		public ICollection<ReviewViewModel> Top6Reviews { get; set; }
		public int NumberOfVotes { get; set; }
	}
}
