using IMDB.Data.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;

namespace IMDB.Services
{
	public sealed class MovieServices : IMovieServices
	{
		private IMovieRepo movieRepo;
		private ILoginSession loginSession;

		public MovieServices(IMovieRepo movieRepo, ILoginSession loginSession)
		{
			this.movieRepo = movieRepo;
			this.loginSession = loginSession;
		}
		public void CreateMovie(string name, string genre, string producer)
		{
			var movieToAdd = movieRepo.GetMovie(name, producer);
			if(movieToAdd == null)
			{
				this.movieRepo.AddMovie(name, genre, producer);
			}
			else
			{
				if (movieToAdd.IsDeleted == false)
					movieRepo.UnDeleteMovie(movieToAdd.ID);
				else
					throw new MovieExistsException();
			}
		}

		public void DeleteMovie(int movieID)
		{
			var movieToDelete = this.movieRepo.GetMovie(movieID);
			if (movieToDelete is null)
				throw new MovieNotFoundException();
			movieRepo.DeleteMovie(movieID);
		}
	}
}
