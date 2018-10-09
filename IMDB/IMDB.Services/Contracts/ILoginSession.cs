using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
	public interface ILoginSession
	{
		string LoggedUser { get; set; }
		string LoggedUserRole { get; set; }
		int LoggedUserRank { get; set; }
		ICollection<string> LoggedUserPermissions { get; set; }
	}
}
