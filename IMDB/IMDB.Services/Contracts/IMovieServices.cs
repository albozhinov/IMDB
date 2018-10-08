namespace IMDB.Services.Contracts
{
	public interface IMovieServices
	{
		void CreateMovie(string name, string genre, string producer);
		void DeleteMovie();
	}
}
