using System.Collections.Generic;

namespace IMDB.Console.Contracts
{
    public interface ICommand
    {
        string Run(IList<string> parameters);
    }
}
