namespace IMDB.Console.Contracts
{
    public interface IUIWriter
    {
        void Write(string message);

        void WriteLine(string message);
    }
}
