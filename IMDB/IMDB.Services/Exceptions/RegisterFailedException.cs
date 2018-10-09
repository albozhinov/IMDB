using System;
using System.Runtime.Serialization;

namespace IMDB.Services.Exceptions
{
	[Serializable]
	internal class RegisterFailedException : Exception
	{
		public RegisterFailedException()
		{
		}

		public RegisterFailedException(string message) : base(message)
		{
		}

		public RegisterFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected RegisterFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}