using System.Collections.Generic;

namespace IMDB.Data.Views
{
	public sealed class MovieView
	{
		public string Name { get; set; }
		public double Score { get; set; }
		public string Producer { get; set; }
		public ICollection<string> Genres { get; set; }
		public ICollection<ReviewView> Top5Reviews { get; set; }

	}
}
