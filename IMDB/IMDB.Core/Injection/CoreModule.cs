using Autofac;
using System.Reflection;

namespace IMDB.Core.Injection
{
	public sealed class CoreModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.AsImplementedInterfaces();

			//RegisterCoreComponents(builder);
			//RegisterCommands(builder);

			base.Load(builder);
		}
	}
}
