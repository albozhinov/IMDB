namespace IMDB.Console.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string inputLine);
    }
}
