using IMDB.Data.Context;
using IMDB.Data.Models;
using IMDB.Services.Contracts;
using IMDB.Services.Exceptions;
using IMDB.Services.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using IMDB.Data.Repository;

namespace IMDB.Services
{
	public sealed class UserServices : IUserServices
	{
        private IRepository<Permissions> permoRepo;
        private IRepository<User> userRepo;
		private ILoginSession loginSession;

		private const int MAX_PASS_LENGTH = 16;
		private const int MIN_PASS_LENGTH = 4;
		private const int MAX_USERNAME_LENGTH = 23;
		private const int MIN_USERNAME_LENGTH = 5;

		private const int GUEST_ID = 0;
		public UserServices(ILoginSession loginSession, IRepository<User> userRepo, IRepository<Permissions> permoRepo)
		{
            this.permoRepo = permoRepo;
            this.userRepo = userRepo;
			this.loginSession = loginSession;
		}

		private ICollection<string> GetPermissions(int role)
		{
			return this.permoRepo.All().Where(p => p.Rank <= role).Select(p => p.Text).ToList();
		}
		//Login with existing user - works
		//Login with non existing user - works
		public void Login(string userName, string password)
		{
			Validator.IfNull<LoginFailedException>(userName, "Password cannot be empty!");	
			Validator.IfNull<LoginFailedException>(password, "Username cannot be empty!");
            Validator.ContainsWhiteSpaces(userName, "UserName can't have empty spaces!");
            Validator.ContainsWhiteSpaces(password, "UserName can't have empty spaces!");

            password = Sha256(password);

			var user = userRepo.All().FirstOrDefault(u => u.UserName == userName && u.Password == password);
			if (user is null) throw new LoginFailedException("User not found or wrong password!");

			this.loginSession.LoggedUserID = user.ID;
			this.loginSession.LoggedUserRole = (UserRoles)user.Rank;
			this.loginSession.LoggedUserPermissions = GetPermissions(user.Rank);
		}

		public void Logout()
		{
            if (!loginSession.LoggedUserPermissions.Contains(System.Reflection.MethodBase.GetCurrentMethod().Name.ToLower()))
                throw new NotEnoughPermissionException("How exactly did you get the idea of logout when you have not logged in?");
			this.loginSession.LoggedUserPermissions = GetPermissions((int)UserRoles.Guest);
			this.loginSession.LoggedUserRole = UserRoles.Guest;
            this.loginSession.LoggedUserID = GUEST_ID;
		}

		public void Register(string userName, string password)
		{
			Validator.IfNull<RegisterFailedException>(userName, "Password cannot be empty!");
			Validator.IfNull<RegisterFailedException>(password, "Username cannot be empty!");
			Validator.IfIsNotInRangeInclusive<int>(password.Length, MIN_PASS_LENGTH, MAX_PASS_LENGTH, "Password should be between 4 and 16 symbols long!");
			Validator.IfIsNotInRangeInclusive<int>(userName.Length, MIN_USERNAME_LENGTH, MAX_USERNAME_LENGTH, "Username should be between 4 and 16 symbols long!");
			Validator.ContainsWhiteSpaces(userName, "UserNames cannot contain spaces!");
			Validator.ContainsWhiteSpaces(password, "UserNames cannot contain spaces!");

            password = Sha256(password);

			if (userRepo.All().Any(u => u.UserName == userName)) throw new RegisterFailedException("Username already exists!");

			var userToAdd = new User
			{
				UserName = userName,
				Password = password,
				Rank = (int)UserRoles.User,
			};
			this.userRepo.Add(userToAdd);
			userRepo.Save();
		}

		private static string Sha256(string randomString)
		{
			Validator.IfNull<ArgumentNullException>(randomString);

			var crypt = new System.Security.Cryptography.SHA256Managed();
			var hash = new StringBuilder();
			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
			foreach (byte theByte in crypto)
			{
				hash.Append(theByte.ToString("x2"));
			}
			return hash.ToString();
		}
	}
}
