using IMDB.Console.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.ConsoleProviders
{
    public class ConsoleWriter : IWriter
    {
        public void WirteLine(string message)
        {
            System.Console.Write(message);
        }

        public void Write(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
