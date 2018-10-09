using IMDB.Data.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;

namespace IMDB.Services
{
	public sealed class UserServices : IUserServices
	{
		private IUserRepo userRepo;
		private ILoginSession loginSession;

		public UserServices(IUserRepo userRepo, ILoginSession loginSession)
		{
			this.userRepo = userRepo;
			this.loginSession = loginSession;

			this.loginSession.LoggedUserPermissions = userRepo.GetPermissions(null);
		}
		public void Login(string userName, string password)
		{
			var user = userRepo.LoginUser(userName, password);
			if (user is null) throw new LoginFailedException();

			this.loginSession.LoggedUserPermissions = userRepo.GetPermissions(user.Name);
			this.loginSession.LoggedUserRole = user.Role;
		}

		public void Logout()
		{
			this.loginSession.LoggedUserPermissions = userRepo.GetPermissions(null);
			this.loginSession.LoggedUserRole = null;
		}

		public void Register(string userName, string password)
		{
			if (this.userRepo.Exists(userName)) throw new RegisterFailedException();

			this.userRepo.AddUser(userName, password);
		}
	}
}
