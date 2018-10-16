using IMDB.Console.Contracts;
using System;

namespace IMDB.Console.ConsoleProviders
{
    public sealed class CommandParser : ICommandParser
    {
        private const string CMD_NOT_FOUND = "Command not found!";
        private IIOCProvider ioc;
        public CommandParser(IIOCProvider ioc)
        {
            this.ioc = ioc;
        }
        public ICommand ParseCommand(string commandName)
        {
            try
            {
                return ioc.ResolveNamed<ICommand>(commandName.ToLower());
            }
            catch (NotImplementedException)
            {
                throw new NotImplementedException(CMD_NOT_FOUND);
            }
        }
    }
}
