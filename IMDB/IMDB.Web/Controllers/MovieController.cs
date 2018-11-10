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
using Microsoft.Extensions.Caching.Memory;

namespace IMDB.Web.Controllers
{
	public class MovieController : Controller
	{
		private readonly IMemoryCache _memoryCache;
		private readonly IMovieServices movieServices;
		public MovieController(IMovieServices movieServices, IMemoryCache memoryCache)
		{
			this._memoryCache = memoryCache;
			this.movieServices = movieServices;
		}
		public async Task<IActionResult> Index()
		{
			//to be cached
			var cachedTopMovies = await _memoryCache.GetOrCreateAsync("TopMovies", async entry =>
			{
				entry.SlidingExpiration = TimeSpan.FromHours(2);
				var movies = await movieServices.GetAllMoviesAsync();
				return movies.OrderByDescending(m => m.MovieScore)
									.Take(10)
									.Select(m => new MovieViewModel(m))
									.ToList();
			});

			return View(cachedTopMovies);
		}

		[HttpGet]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Create()
		{
			var cachedSelectListGenres = await _memoryCache.GetOrCreateAsync("Genres", async entry =>
			{
				entry.SlidingExpiration = TimeSpan.FromHours(4);
				var movieGenres = await movieServices.GetGenresAsync();
				return movieGenres.Select(g => new SelectListItem(g.GenreType, g.GenreType));
			});
			var newMovie = new MovieViewModel
			{
				GenreList = cachedSelectListGenres
			};
			return View(newMovie);
		}
		[HttpPost]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Create(MovieViewModel movieViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View();
			}
			var newMovie = await movieServices.CreateMovieAsync(movieViewModel.Name, movieViewModel.Genres, movieViewModel.Director);
			return this.RedirectToAction("Details", "Players", new { id = newMovie.ID });
		}
        [HttpGet("[controller]/[action]/{id}")]        
        public async Task<IActionResult> Details(int id)
        {
            //return single detailed movie view            
            try
            {
                var foundMovie = await this.movieServices.CheckMovieAsync(id);
                return View(new MovieViewModel(foundMovie));
            }
            catch (MovieNotFoundException)
            {
                return this.NotFound();
            }
            catch(ArgumentException)
            {
                return this.NotFound();
            }
        }
        [HttpDelete("[controller]/Details/{id}")]
		[Authorize(Roles = "Administrator")]
		public IActionResult DeleteMovie(int id)
		{
			//return single detailed movie view
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Search(SearchViewModel searchView)
		{
            var movieName = searchView.Name;
            var movieGenre = searchView.Genres;
            var movieDirector = searchView.Director;
			var moviesAwaited = await movieServices
				.SearchMovieAsync(movieName, movieGenre, movieDirector);
            return View("SearchResult", moviesAwaited
								.Select(m => new MovieViewModel(m))
								.ToList());
		}

        public IActionResult Search()
        {
            return View();
        }

        [HttpGet("[controller]/{id}/[action]")]
		public IActionResult Rate(int id)
		{
			return View();
		}
		[Route("[controller]/{id}/reviews")]
		public IActionResult Reviews(int id)
		{
			return View();
		}
        
    }
}