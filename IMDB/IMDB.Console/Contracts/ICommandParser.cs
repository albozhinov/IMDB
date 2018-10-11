namespace IMDB.Console.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandName);
    }
}
