using IMDB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Web.Models
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public uint ItemsPerPage { get; set; }
        public ICollection<string> Genres { get; set; }

        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
