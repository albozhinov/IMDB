using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Services.Exceptions
{
    public class ReviewNotFoundException : Exception
    {
        public ReviewNotFoundException(string message)
            : base(message)
        {
                
        }
    }
}
