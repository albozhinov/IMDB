using IMDB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Web.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<User> users)
        {
            this.Users = users.Select(u => new UserViewModel(u));
        }
        public string StatusMessage { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; } 
    }
}
