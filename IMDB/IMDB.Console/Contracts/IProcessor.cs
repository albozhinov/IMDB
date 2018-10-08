using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.Contracts
{
    public interface IProcessor
    {
        string ProcessCommand(string inputLine);
    }
}
