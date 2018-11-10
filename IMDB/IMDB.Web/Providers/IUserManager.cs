using IMDB.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Web.Providers
{
    public interface IUserManager<T>
    {
        Task<IdentityResult> SetLockoutEndDateAsync(T usrer, DateTimeOffset? lockoutEnd);
        Task<IdentityResult> AddPasswordAsync(T user, string password);
        Task<IdentityResult> RemovePasswordAsync(T user);

    }
}
