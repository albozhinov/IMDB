using IMDB.Data.Models;
using IMDB.Web.Areas.Admin.Models;
using IMDB.Web.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace IMDB.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly IUserManager<User> _userManager;
        private readonly int PAGE_SIZE = 1;

        [TempData]
        public string StatusMessage { get; set; }

        public UsersController(IUserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel(_userManager.Users, 1, PAGE_SIZE);
            indexViewModel.StatusMessage = StatusMessage;

            return View(indexViewModel);
        }
        public IActionResult UserGrid(int? page)
        {
            var pagedUsers = _userManager.Users
                                         .Select(u => new UserViewModel(u))
                                         .ToPagedList(page ?? 1, PAGE_SIZE);
            return PartialView("_UserGrid", pagedUsers);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(UserModalModelView input)
        {
            var user = _userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return this.RedirectToAction(nameof(Index));
            }

            var enableLockOutResult = await _userManager.SetLockoutEnabledAsync(user, true);
            if (!enableLockOutResult.Succeeded)
            {
                this.StatusMessage = "Error: Could enable the lockout on the user!";
                return this.RedirectToAction(nameof(Index));
            }
            var lockoutTimeResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));
            if (!lockoutTimeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not add time to user's lockout!";
                return this.RedirectToAction(nameof(Index));
            }
            this.StatusMessage = "The user has been successfully locked for 10 years!";
            return this.RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(UserModalModelView input)
        {
            var user = _userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return this.RedirectToAction(nameof(Index));
            }

            var lockoutTimeResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);
            if (!lockoutTimeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not add time to user's lockout!";
                return this.RedirectToAction(nameof(Index));
            }
            this.StatusMessage = "The user has been successfully unlocked!";
            return this.RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserModalModelView input)
        {
            var user = _userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
            if (user is null)
            {
                this.StatusMessage = "Error: User not found!";
                return this.RedirectToAction(nameof(Index));
            }

            foreach (var validator in _userManager.PasswordValidators)
            {
                var result = await validator.ValidateAsync(_userManager.Instance, user, input.ConfirmPassword);
                if (!result.Succeeded)
                {
                    this.StatusMessage = $"Error: {string.Join(" ", result.Errors.Select(e => e.Description)).Replace(".", "!")}";
                    return this.RedirectToAction(nameof(Index));
                }
            }

            if (!ModelState.IsValid)
            {
                this.StatusMessage = "Error: Passwords do not match!";
                return this.RedirectToAction(nameof(Index));
            }

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not remove the old password!";
                return this.RedirectToAction(nameof(Index));
            }
            var addPasswordResult = await _userManager.AddPasswordAsync(user, input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                this.StatusMessage = "Error: Could not change the password!";
                return this.RedirectToAction(nameof(Index));
            }
            this.StatusMessage = "The user's password has been changed!";
            return this.RedirectToAction(nameof(Index));
        }
    }
}