namespace IMDB.Data.Views
{
	public sealed class ReviewView
	{
		public double Rating { get; set; }
		public double Score { get; set; }
		public string Text { get; set; }
		public string ByUser { get; set; }
		//Is it ok?
		public string MovieName { get; set; }
	}
}
