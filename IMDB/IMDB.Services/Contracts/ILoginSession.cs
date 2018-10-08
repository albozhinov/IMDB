using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
	public interface ILoginSession
	{
		string LoggedUserRole { get; set; }
		ICollection<string> LoggedUserPermissions { get; set; }
	}
}
