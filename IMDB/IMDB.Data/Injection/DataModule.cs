using Autofac;
using System.Reflection;

namespace IMDB.Data.Injection
{
	public sealed class DataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.AsImplementedInterfaces();


			base.Load(builder);
		}
	}
}
