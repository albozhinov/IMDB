namespace IMDB.Services.Contracts
{
	public interface IExportingServices
	{
        /// <summary>
        /// Exports all movies and their top 5 comments to movie views
        /// </summary>
		void ListMoviesToPDF();
	}
}
