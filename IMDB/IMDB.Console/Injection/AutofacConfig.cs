using Autofac;
using IMDB.Data.Injection;
using IMDB.Services.Injection;
using System.Reflection;

namespace IMDB.Console.Injection
{
    public sealed class AutofacConfig
    {
        public IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(DataModule)));
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(ConsoleModule)));
            builder.RegisterAssemblyModules(Assembly.GetAssembly(typeof(ServicesModule)));

            return builder.Build();
        }
    }
}