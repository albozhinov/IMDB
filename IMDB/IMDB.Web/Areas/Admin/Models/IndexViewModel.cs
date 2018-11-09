using IMDB.Data.Models;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Web.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<User> users, int pageNumber, int pageSize)
        {
            this.Users = users.Select(u => new UserViewModel(u)).ToPagedList(pageNumber, pageSize);
        }
        public string StatusMessage { get; set; }
        public IPagedList<UserViewModel> Users { get; set; } 
    }
}
