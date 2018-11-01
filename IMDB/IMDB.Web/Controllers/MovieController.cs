using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Services.Contracts;
using IMDB.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
			var movies = movieServices.GetAllMovies().Select(m => new MovieViewModel(m)).ToList();
            return View(movies);
        }
		//[HttpPost]
		//public IActionResult Create()
		//{
		//	return View();
		//}
		//[HttpGet]
		//public IActionResult Create(MovieViewModel movieViewModel)
		//{
		//	return View();//Redirect to create movie!
		//}
		[HttpGet("[controller]/{id}")]
		public IActionResult CheckMovie(int id)
		{
			//return single detailed movie view
			return View();
		}
		[HttpDelete("[controller]/{id}")]
		[Authorize(Roles = "Administration")]
		public IActionResult DeleteMovie(int id)
		{
			//return single detailed movie view
			return View();
		}
		[HttpGet("[controller]/search")]
		public IActionResult SearchMovie()
		{
			return View();
		}
        //[HttpPost("[controller]/search")]
        //public IActionResult SearchMovie(SearchViewModel searchView)
        //{
        //	//redirect to search result
        //	return View();
        //}
        //public IActionResult SearchResult(SearchResultViewModel searchResultView)
        //{
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