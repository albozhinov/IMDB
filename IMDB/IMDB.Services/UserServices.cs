using IMDB.Data.Contracts;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
	public sealed class UserServices : IUserServices
	{
		private IRepository<User> userRepo;
		private ILoginSession loginSession;

		public UserServices(IRepository<User> userRepo, ILoginSession loginSession)
		{
			this.userRepo = userRepo;
			this.loginSession = loginSession;

			this.loginSession.LoggedUserPermissions = GetPermissions(null);
		}

		private ICollection<string> GetPermissions(string userName)
		{
			//TODO fix and add null thingy
			return this.userRepo.All().Where(u => u.Rank <= loginSession.LoggedUserRank).Select(u => userName).ToList();
		}
		public void Login(string userName, string password)
		{
			/*var user = userRepo.LoginUser(userName, password);
			if (user is null) throw new LoginFailedException();
            
			this.loginSession.LoggedUserPermissions = GetPermissions(user.Name);
			this.loginSession.LoggedUserRole = user.Role;
			this.loginSession.LoggedUserRank = user.Rank;*/
		}

		public void Logout()
		{
			this.loginSession.LoggedUserPermissions = GetPermissions(null);
			this.loginSession.LoggedUserRole = null;
		}

		public void Register(string userName, string password)
		{
			/*if (this.userRepo.Exists(userName)) throw new RegisterFailedException();

			this.userRepo.AddUser(userName, password);*/
		}
	}
}
