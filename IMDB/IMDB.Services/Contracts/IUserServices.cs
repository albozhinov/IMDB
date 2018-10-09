namespace IMDB.Services.Contracts
{
	public interface IUserServices
	{
		/// <summary>
		/// Registers a user
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		void Register(string userName, string password);
		/// <summary>
		/// Logins a user
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="password"></param>
		void Login(string userName, string password);
		/// <summary>
		/// Logouts a user
		/// </summary>
		void Logout();
	}
}
