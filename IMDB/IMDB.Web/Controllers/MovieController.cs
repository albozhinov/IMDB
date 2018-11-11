using System;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Web.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using X.PagedList;

namespace IMDB.Web.Controllers
{
	public class MovieController : Controller
	{
        private readonly IUserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;
		private readonly IMovieServices movieServices;
        private string StatusMessage { get; set; }

		public MovieController(IMovieServices movieServices, IMemoryCache memoryCache, IUserManager<User> userManager)
		{
            this._userManager = userManager;
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
        public async Task<IActionResult> Top5Movies()
        {
            var cachedTop5Movies = await _memoryCache.GetOrCreateAsync("TopMovies", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(2);
                var movies = await movieServices.GetAllMoviesAsync();
                return movies.OrderByDescending(m => m.MovieScore)
                                    .Take(5)
                                    .Select(m => new MovieViewModel(m))
                                    .ToList();
            });
            return View(cachedTop5Movies);
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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(SearchViewModel searchView, int? page)
		{
            var movieName = searchView.Name;
            var movieGenre = searchView.Genres;
            var movieDirector = searchView.Director;
			var moviesAwaited = await movieServices
				.SearchMovieAsync(movieName, movieGenre, movieDirector);
            return PartialView("_SearchResult", moviesAwaited
                                .Select(m => new MovieViewModel(m))
                                .Take(50)
                                .ToPagedList(page ?? 1, (int)searchView.ItemsPerPage));
		}

        public async Task<IActionResult> Search()
        {
            var cachedSelectListGenres = await _memoryCache.GetOrCreateAsync("Genres", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromHours(4);
                var movieGenres = await movieServices.GetGenresAsync();
                return movieGenres.Select(g => new SelectListItem(g.GenreType, g.GenreType));
            });
            var searchVM = new SearchViewModel
            {
                GenreList = cachedSelectListGenres
            };
            return View(searchVM);
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