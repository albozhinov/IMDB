using System.Collections.Generic;
using IMDB.Services.Contracts;
using PingPong.Providers.Validation;
using System;

namespace IMDB.Services
{
	public class LoginSession : ILoginSession
	{
		private string loggedUser;
		private int loggedUserID;
		private ICollection<string> loggedUserPermissions;
		public int LoggedUserID
		{
			get => loggedUserID;
			set
			{
				Validator.IsNonNegative(value, "ID Cannot be negative!");
				loggedUserID = value;
			}
		}
		public string LoggedUser
		{
			get => loggedUser;
			set
			{
				Validator.IfNull<ArgumentNullException>(value, "LoggedUser cannot be null!");
				this.loggedUser = value;
			}
		}
		public UserRoles LoggedUserRole { get; set; }
		public ICollection<string> LoggedUserPermissions
		{
			get
			{
				return new List<string>(loggedUserPermissions);
			}
			set
			{
				Validator.IfNull<ArgumentNullException>(value, "Permissions cananot be null!");
				this.loggedUserPermissions = value;
			}
		}
	}
}
