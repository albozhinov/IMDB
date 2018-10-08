using IMDB.Data.Contracts;
using IMDB.Services.Contracts;

namespace IMDB.Services
{
	public sealed class UserServices : IUserServices
	{

		public UserServices(IUserRepo userReop)
		{

		}
		public void Login(string userName, string password)
		{

		}

		public void Logout()
		{
			throw new System.NotImplementedException();
		}

		public void Register(string userName, string password)
		{
			throw new System.NotImplementedException();
		}
	}
}
