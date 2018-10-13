using IMDB.Console.Contracts;
using System.Linq;

namespace IMDB.Console.ConsoleProviders
{
    public sealed class CommandProcessor : ICommandProcessor
    {
        private ICommandParser parser;
        public CommandProcessor(ICommandParser parser)
        {
            this.parser = parser;
        }
        public string ProcessCommand(string inputLine)
        {
            //processes the command (parses the parrameters and gives it to the command)
            //command format: cmdname - <arg1> : <arg2> : <arg3> : ...
            var cmdArguments = inputLine.Split('-').Select(arg => arg.Trim()).ToList();
            ICommand command = parser.ParseCommand(cmdArguments.First().Replace(" ", ""));
            if (cmdArguments.Count < 2)
                return "Thats a nice potential command, but please, put '-' after it";
            var parameters = cmdArguments[1].Split(':').Select(arg => arg.Trim()).ToList();
            return command.Run(parameters);
        }
    }
}
