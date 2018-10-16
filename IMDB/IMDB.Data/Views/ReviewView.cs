using System.Text;

namespace IMDB.Data.Views
{
	public class ReviewView
	{
        public int ReviewID { get; set; }
		public double Rating { get; set; }
		public double Score { get; set; }
		public string Text { get; set; }
		public string ByUser { get; set; }
		public string MovieName { get; set; }
		public int NumberOfVotes { get; set; }
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"#{ReviewID} |Movie Rating: {this.Rating}| |Score: {this.Score:F2}| |By User: {this.ByUser}| |{NumberOfVotes} Vote(s)");
			sb.AppendLine($"   {this.Text}");
			return sb.ToString();
		}
	}
}
