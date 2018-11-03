using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
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
            var movies = movieServices.GetAllMovies();
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
        //[HttpPost]
        //public IActionResult SearchMovie(SearchViewModel searchView)
        //{
        //	//redirect to search result
        //	return View();
        //}
        //public IActionResult SearchResult(SearchResultViewModel searchResultView)
        //{
        //	return View();
        //}
        //[Route("~/{id}/rate")]
        //[HttpGet]
        //public IActionResult Rate(int id)
        //{
        //	return View();
        //}
        //[HttpPost]
        //public IActionResult Rate(RateViewModel rateViewModel)
        //{
        //	return View();//redirect to review
        //}
        //[Route("~/{id}/reviews")]
        //public IActionResult Reviews(int id)
        //{
        //	return View();
        //}
    }
}