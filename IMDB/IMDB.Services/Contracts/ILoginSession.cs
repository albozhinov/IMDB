using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
	public interface ILoginSession
	{
		string LoggedUser { get; set; }
		int LoggedUserID { get; set; }
		UserRoles LoggedUserRole { get; set; }
		ICollection<string> LoggedUserPermissions { get; set; }
	}
}
