namespace IMDB.Console.Contracts
{
    public interface IIOCProvider
    {
        T ResolveNamed<T>(string serviceName);
    }
}
