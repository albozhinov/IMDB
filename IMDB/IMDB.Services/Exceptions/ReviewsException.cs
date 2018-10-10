using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Services.Exceptions
{
    public sealed class ReviewsException : Exception        
    {
        public ReviewsException(string message)
            : base(message)
        {

        }
    }
}
