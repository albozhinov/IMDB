using IMDB.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Email = user.Email;
            UserName = user.UserName;
            ID = user.Id;

        }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ID { get; set; }

    }
}
