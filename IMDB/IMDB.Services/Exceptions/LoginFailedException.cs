using System;
namespace IMDB.Services.Exceptions
{
	public sealed class LoginFailedException : Exception
	{
		public LoginFailedException(string message)
			: base(message)
		{

		}
	}
}
