using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Views
{
	public class MovieView
	{
        public int ID { get; set; }
		public string Name { get; set; }
		public double Score { get; set; }
		public string Director { get; set; }
		public ICollection<string> Genres { get; set; }
		public ICollection<ReviewView> Top5Reviews { get; set; }
		public int NumberOfVotes { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.ID}# |Movie: {this.Name}| |Director: {this.Director}| |Movie Score: {this.Score}| |{NumberOfVotes} Vote(s)");
            sb.AppendLine(" Genre: " + String.Join(", ", this.Genres));
            sb.AppendLine();
            sb.AppendLine("Top 5 reviews: ");
            foreach (var review in this.Top5Reviews)
            {
                sb.AppendLine(review.ToString());
            }
            return sb.ToString();
        }

    }
}
