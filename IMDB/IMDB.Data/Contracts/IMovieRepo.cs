
namespace IMDB.Data.Contracts
{
	public interface IMovieRepo
	{
		void AddMovie(string name, string genre, string producer);
		
	}
}
