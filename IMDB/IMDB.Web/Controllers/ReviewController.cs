using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace IMDB.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewsServices reviewsServices;
        private readonly UserManager<User> _userManager;

        public ReviewController(IReviewsServices reviewsServices, UserManager<User> userManager)
        {
            this.reviewsServices = reviewsServices;
            this._userManager = userManager;
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
                reviews.Select(rev => rev.MovieId == id);

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
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteReview(int id, int movieId, string action, string controller)
        {
            this.reviewsServices.DeleteReview(id);
            return RedirectToAction(action, controller, new { id = movieId});
        }
        //[HttpPost]
        //[Authorize]
        //public IActionResult AddReview(int id)
        //{
        //    this.User
        //    this.userManager.GetUserId(HttpContext.User.);
        //}

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RateReview(int id, double rate, int movieId, string action, string controller)
        {
            var userId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(HttpContext.User));
            this.reviewsServices.RateReview(id, rate, userId);
            return RedirectToAction(action, controller, new { id = movieId});
        }
    }
}