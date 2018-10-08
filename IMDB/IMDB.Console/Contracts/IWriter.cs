using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Console.Contracts
{
    public interface IWriter
    {
        void Write(string message);

        void WirteLine(string message);
    }
}
