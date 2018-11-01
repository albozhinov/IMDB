using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Web.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
			//show all movies
            return View();
        }
		[HttpPost]
		public IActionResult Create()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create(MovieViewModel movieViewModel)
		{
			return View();//Redirect to create movie!
		}
		[HttpGet]
		[Route("~/{id}")]
		public IActionResult CheckMovie(int id)
		{
			//return single detailed movie view
			return View();
		}
		[HttpDelete]
		[Route("~/{id}")]
		[Authorize("Admin")]//?? policy or role
		public IActionResult DeleteMovie(int id)
		{
			//return single detailed movie view
			return View();
		}
		[HttpGet]
		public IActionResult SearchMovie()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SearchMovie(SearchViewModel searchView)
		{
			//redirect to search result
			return View();
		}
		public IActionResult SearchResult(SearchResultViewModel searchResultView)
		{
			return View();
		}
		[Route("~/{id}/rate")]
		[HttpGet]
		public IActionResult Rate(int id)
		{
			return View();
		}
		[HttpPost]
		public IActionResult Rate(RateViewModel rateViewModel)
		{
			return View();//redirect to review
		}
		[Route("~/{id}/reviews")]
		public IActionResult Reviews(int id)
		{
			return View();
		}
	}
}