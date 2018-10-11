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
		private const string DEFAULT_USER_REGISTER_ROLE = "user";
		private const int DEFAULT_USER_REGISTER_RANK = 1;

		public UserServices(IRepository<User> userRepo, ILoginSession loginSession)
		{
			this.userRepo = userRepo;
			this.loginSession = loginSession;

			this.loginSession.LoggedUserPermissions = GetPermissions(-1);
			this.loginSession.LoggedUserID = -1;
			this.loginSession.LoggedUserRank = 0;
		}

		private ICollection<string> GetPermissions(int userID)
		{
			//gets the permissions of guest
			if(userID == -1)
				return this.userRepo.All().Where(u => u.Rank <= loginSession.LoggedUserRank).Select(u => u.UserName).ToList();
			//gets the permissions of a user
			return this.userRepo.All().Where(u => u.Rank <= loginSession.LoggedUserRank).Select(u => u.UserName).ToList();
		}
		public void Login(string userName, string password)
		{
			//TODO validate userName, password for null
			var user = userRepo.All().FirstOrDefault(u => u.UserName == userName && u.Password == password);
			if (user is null) throw new LoginFailedException("User not found or wrong password!");
            
			this.loginSession.LoggedUserPermissions = GetPermissions(user.ID);
			this.loginSession.LoggedUserRole = user.Role;
			this.loginSession.LoggedUserRank = user.Rank;
			this.loginSession.LoggedUserID = user.ID;
		}

		public void Logout()
		{
			this.loginSession.LoggedUserPermissions = GetPermissions(-1);
			this.loginSession.LoggedUserRole = null;
			this.loginSession.LoggedUserID = -1;
			//This is guest login ID ^
			this.loginSession.LoggedUserRank = 0;
		}

		public void Register(string userName, string password)
		{
			//TODO validate username, password for registration format
			if (this.userRepo.All().Any(u => u.UserName == userName)) throw new RegisterFailedException("Username already exists!");

			var userToAdd = new User
			{
				UserName = userName,
				Password = password,
				Rank = DEFAULT_USER_REGISTER_RANK,
				Role = DEFAULT_USER_REGISTER_ROLE
			};
			this.userRepo.Add(userToAdd);
		}
	}
}
