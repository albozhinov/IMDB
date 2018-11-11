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
        public async Task<IActionResult> AllReviews(int id)
        {
            try
            {
				var reviewsRaw = await this.reviewsServices.ListMovieReviewsAsync(id);
                var reviewsViewModels = reviewsRaw
                              .OrderByDescending(rev => rev.ReviewScore)
                              .Select(r => new ReviewViewModel(r))
                              .ToList();
                reviewsViewModels.Select(rev => rev.MovieId == id);

                return View(reviewsViewModels);
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
        public async Task<IActionResult> DeleteReview(int id, int movieId)
        {
            await this.reviewsServices.DeleteReviewAsync(id);
            return RedirectToAction("Details", "Movie", new { id = movieId});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RateReview(int id, double rate)
        {
            var userId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(HttpContext.User));
            var ratedReview = await this.reviewsServices.RateReviewAsync(id, rate, userId);
            return RedirectToAction("Details", "Movie", new { id = ratedReview.MovieID});
        }
    }
}