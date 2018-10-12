using IMDB.Console.Contracts;

namespace IMDB.Console.ConsoleProviders
{
    public sealed class ConsoleWriter : IUIWriter
    {
        public void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }

        public void Write(string message)
        {
            System.Console.Write(message);
        }
    }
}
