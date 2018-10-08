namespace IMDB.Console.Contracts
{
	public interface ILoginSession
	{
		void LoginUser(string userName, string type);
		void LogoutUser();
	}
}
