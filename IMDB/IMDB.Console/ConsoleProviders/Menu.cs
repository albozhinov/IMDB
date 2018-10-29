using IMDB.Console.Contracts;
using IMDB.Services.Contracts;

namespace IMDB.Console.ConsoleProviders
{
	public sealed class Menu : IMenu
	{
		private IUIWriter writer;
		private ILoginSession session;
		public Menu(IUIWriter writer, ILoginSession session)
		{
			this.writer = writer;
			this.session = session;
		}
		public void WriteOptions()
		{
			writer.WriteLine("Avaliable commands: ");
			//writer.WriteLine(string.Join(' ', session.LoggedUserPermissions));
		}
	}
}
