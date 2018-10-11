using IMDB.Console.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.ConsoleProviders
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}
