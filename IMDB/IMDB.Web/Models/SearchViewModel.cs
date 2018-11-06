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
        public string Genres { get; set; }
    }
    
}
