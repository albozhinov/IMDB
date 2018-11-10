using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace IMDB.Web.Providers
{

    public class UserManagerWrapper<T> : IUserManager<T> where T: class
    {
        private UserManager<T> _userManager;

        public UserManagerWrapper(UserManager<T> userManager)
        {
            this._userManager = userManager;
        }



        public async Task<IdentityResult> SetLockoutEndDateAsync(T user, DateTimeOffset? lockoutEnd)
        {
            return await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        }
        public async Task<IdentityResult> RemovePasswordAsync(T user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }
        public async Task<IdentityResult> AddPasswordAsync(T user, string password)
        {
            return await _userManager.AddPasswordAsync(user, password);
        }
    }
}
