using System;
using System.Runtime.Serialization;

namespace IMDB.Services.Exceptions
{
	[Serializable]
    public class MovieExistsException : Exception
	{
		public MovieExistsException()
		{
		}

		public MovieExistsException(string message) : base(message)
		{
		}

		public MovieExistsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MovieExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}