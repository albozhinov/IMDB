using Autofac;
using IMDB.Core.Contracts;
using System.Reflection;

namespace IMDB.Core.Injection
{
	public sealed class AutofacConfig
	{
		public IContainer Build()
		{
			var builder = new ContainerBuilder();

			builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(IEngine)));
			return builder.Build();
		}
	}
}
