using Autofac;
using System.Reflection;

namespace IMDB.Console.Injection
{
	public sealed class ConsoleModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.AsImplementedInterfaces();


			base.Load(builder);
		}
	}
}
