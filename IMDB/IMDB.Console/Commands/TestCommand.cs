using IMDB.Console.Contracts;
using System.Collections.Generic;

namespace IMDB.Console.Commands
{
    public sealed class TestCommand : ICommand
    {
        public string Run(IList<string> parameters)
        {
            return "Test succeeded" + " " + "first param" + " " + parameters[0];
        }
    }
}
