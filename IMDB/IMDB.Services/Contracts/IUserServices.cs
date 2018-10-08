namespace IMDB.Services.Contracts
{
	public interface IUserServices
	{
		void Register(string userName, string password);
		void Login(string userName, string password);
		void Logout();
	}
}
