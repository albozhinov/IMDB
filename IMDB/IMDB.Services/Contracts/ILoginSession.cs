using System.Collections.Generic;

namespace IMDB.Services.Contracts
{
	public interface ILoginSession
	{
		string LoggedUserID { get; set; }
		UserRoles LoggedUserRole { get; set; }
	}
}
