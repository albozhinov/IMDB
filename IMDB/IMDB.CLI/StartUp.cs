using Autofac;
using IMDB.Core.Contracts;
using IMDB.Core.Injection;

namespace IMDB.CLI
{
	public sealed class StartUp
	{
		static void Main(string[] args)
		{
			var containerConfig = new AutofacConfig();
			var container = containerConfig.Build();

			var engine = container.Resolve<IEngine>();
			engine.Start();
		}
	}
}
