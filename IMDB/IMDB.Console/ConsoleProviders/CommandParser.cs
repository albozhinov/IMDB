using Autofac;
using Autofac.Core.Registration;
using IMDB.Console.Contracts;
using System;

namespace IMDB.Console.ConsoleProviders
{
    public sealed class CommandParser : ICommandParser
    {
        private const string CMD_NOT_FOUND = "Command not found!";
        private ILifetimeScope scope;
        public CommandParser(ILifetimeScope scope)
        {
            this.scope = scope;
        }
        public ICommand ParseCommand(string commandName)
        {
            //parse the command via autofac
            try
            {
                return scope.ResolveNamed<ICommand>(commandName.ToLower());
            }
            catch (ComponentNotRegisteredException)
            {
                throw new NotImplementedException(CMD_NOT_FOUND);
            }
        }
    }
}
