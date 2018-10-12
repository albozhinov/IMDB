using Autofac;
using IMDB.Console.ConsoleProviders;
using IMDB.Console.Contracts;
using System.Linq;
using System.Reflection;

namespace IMDB.Console.Injection
{
	public sealed class ConsoleModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.AsImplementedInterfaces();

            this.RegisterComponents(builder);
            this.RegisterCommands(builder);

            base.Load(builder);
		}
        private void RegisterComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<CommandParser>().As<ICommandParser>().SingleInstance();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>().SingleInstance();
            builder.RegisterType<ConsoleReader>().As<IUIReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IUIWriter>().SingleInstance();

        }

        public void RegisterCommands(ContainerBuilder builder)
        {
            Assembly assmebly = Assembly.GetAssembly(typeof(ICommand));

            var commandTypes = assmebly.DefinedTypes
                                .Where(x => x.ImplementedInterfaces
                                    .Any(i => i == typeof(ICommand)))
                                .Where(x => !x.IsAbstract);

            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.UnderlyingSystemType)
                    .Named<ICommand>(commandType.Name
                                        .ToLower()
                                        .Replace("command", ""))
                    .SingleInstance();
            }
        }
    }
}
