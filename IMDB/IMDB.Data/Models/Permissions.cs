﻿using System;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Data.Models
{
    public class Permissions
    {
        public int ID { get; set; }
        public String Text { get; set; }
        [Required]
        public int Rank { get; set; }
    }
}
