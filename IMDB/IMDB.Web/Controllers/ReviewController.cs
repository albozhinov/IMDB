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
    public class ReviewController : Controller
    {
        private readonly IReviewsServices reviewsServices;

        public ReviewController(IReviewsServices reviewsServices)
        {
            this.reviewsServices = reviewsServices;
        }

        [HttpGet("[controller]/[action]/{id}")]
        public IActionResult AllReviews(int id)
        {
            try
            {
                var reviews = this.reviewsServices
                              .ListMovieReviews(id)
                              .Take(50)
                              .OrderByDescending(rev => rev.ReviewScore)
                              .Select(r => new ReviewViewModel(r))
                              .ToList();

                return View(reviews);
            }
            catch (MovieNotFoundException)
            {
                return this.NotFound();
            }
            catch (ArgumentException)
            {
                return this.NotFound();
            }
        }
        [HttpPost("[controller]/[action]/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteReview(int id)
        {           
            this.reviewsServices.DeleteReview(id);
            this.TempData["Succes-Message"] = "You deleted a review with ID: " + id;
            return RedirectToAction("Index", "Movie");
        }
    }
}