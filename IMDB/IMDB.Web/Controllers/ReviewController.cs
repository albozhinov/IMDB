using System;
using System.Linq;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int id, string currentAction, string currentController)
        {           
           var deletedReview = await this.reviewsServices.DeleteReviewAsync(id);
            return RedirectToAction(currentAction, currentController, new { id = deletedReview.MovieID});
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateReview(int id, double rate, string currentAction, string currentController)
        {            
            var userId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(HttpContext.User));
            var ratedReview = await this.reviewsServices.RateReviewAsync(id, rate, userId);
            return RedirectToAction(currentAction, currentController, new { id = ratedReview.MovieID});
        }
    }
}