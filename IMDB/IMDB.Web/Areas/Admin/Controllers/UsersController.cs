using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Data.Models;
using IMDB.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        [Area("Admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var indexViewModel = new IndexViewModel(_userManager.Users);
            indexViewModel.StatusMessage = "You succesfully entered the admin area!";
            return View(indexViewModel);
        }
    }
}