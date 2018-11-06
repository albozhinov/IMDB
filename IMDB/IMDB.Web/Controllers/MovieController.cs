using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMDB.Web.Controllers
{
	public class MovieController : Controller
	{
		private readonly IMovieServices movieServices;
		public MovieController(IMovieServices movieServices)
		{
			this.movieServices = movieServices;
		}
		public IActionResult Index()
		{
			//to be cached
			var movies = movieServices
				.GetAllMovies()
				.OrderByDescending(m => m.MovieScore)
				.Take(10)
				.Select(m => new MovieViewModel(m))
				.ToList();
			return View(movies);
		}
		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public IActionResult Create()
		{
			var newMovie = new MovieViewModel
			{
				GenreList = movieServices.GetGenres().Select(g => new SelectListItem(g.GenreType, g.GenreType))
			};
			return View(newMovie);
		}
		[HttpPost]
		//[Authorize(Roles = "Administrator")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(MovieViewModel movieViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View();
			}
			var newMovie = movieServices.CreateMovie(movieViewModel.Name, movieViewModel.Genres, movieViewModel.Director);
			return this.RedirectToAction("Details", "Players", new { id = newMovie.ID });
		}
        [HttpGet("[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            //return single detailed movie view            
            try
            {
                var foundMovie = this.movieServices.CheckMovie(id);
                return View(new MovieViewModel(foundMovie));
            }
            catch (MovieNotFoundException)
            {
                return this.NotFound();
            }
        }
        [HttpDelete("[controller]/Details/{id}")]
		[Authorize(Roles = "Administration")]
		public IActionResult DeleteMovie(int id)
		{
			//return single detailed movie view
			return View();
		}

		[HttpPost]
		public IActionResult Search(SearchViewModel searchView)
		{
            var movieName = searchView.Name;
            var movieGenre = searchView.Genres;
            var movieDirector = searchView.Director;
            var movies = movieServices
                .SearchMovie(movieName, movieGenre, movieDirector)
                .Select(mov=>new MovieViewModel(mov));
            return View("SearchResult", movies.ToList());
            //return SearchResault(movies.ToList());
		}

        public IActionResult Search()
        {
            return View();
        }
        /*[HttpGet]
        private IActionResult SearchResault(ICollection <MovieViewModel> movies)
        {
            return View(movies);
        }*/
        //[HttpPost("[controller]/search")]
        //public IActionResult SearchMovie(SearchViewModel searchView)
        //{
        //	//redirect to search result
        //	return View();
        //}

        [HttpGet("[controller]/{id}/[action]")]
		public IActionResult Rate(int id)
		{
			return View();
		}
		//[HttpPost("[controller]/{id}/[action]")]
		//public IActionResult Rate(RateViewModel rateViewModel)
		//{
		//	return View();//redirect to review
		//}
		[Route("[controller]/{id}/reviews")]
		public IActionResult Reviews(int id)
		{
			return View();
		}
        
    }
}