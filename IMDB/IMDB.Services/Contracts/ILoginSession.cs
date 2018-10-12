using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
	public interface ILoginSession
	{
		int LoggedUserID { get; set; }
		UserRoles LoggedUserRole { get; set; }
		ICollection<string> LoggedUserPermissions { get; set; }
	}
}
