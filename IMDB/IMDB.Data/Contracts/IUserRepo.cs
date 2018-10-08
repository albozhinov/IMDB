using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data.Contracts
{
	public interface IUserRepo
	{
		void AddUser(string userName, string password);
		bool Exists(string userName);
		ICollection<string> GetPermissions(string userName);
		bool LoginUser(string userName);
	}
}
