using Autofac;
using IMDB.Data.Context;

namespace IMDB.Data.Injection
{
	public sealed class DataModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<IMDBContext>().AsSelf();
			base.Load(builder);

            builder.RegisterType<IMDBContext>().AsSelf();

        }
    }
}
