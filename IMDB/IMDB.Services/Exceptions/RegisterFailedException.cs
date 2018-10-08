using System;

namespace IMDB.Services.Exceptions
{
	public sealed class RegisterFailedException : Exception
	{
		public RegisterFailedException(string message)
			: base(message)
		{

		}
	}
}
