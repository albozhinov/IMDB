using IMDB.Console.Contracts;

namespace IMDB.Console.ConsoleProviders
{
    public sealed class ConsoleReader : IUIReader
    {
        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}
