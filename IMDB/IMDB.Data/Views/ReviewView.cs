using System.Text;

namespace IMDB.Data.Views
{
	public sealed class ReviewView
	{
		public double Rating { get; set; }
		public double Score { get; set; }
		public string Text { get; set; }
		public string ByUser { get; set; }
		public string MovieName { get; set; }
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"# |Movie Rating: {this.Rating}| |Score: {this.Score}| |By: {this.ByUser}|");
			sb.AppendLine($"   {this.Text}");
			return sb.ToString();
		}
	}
}
