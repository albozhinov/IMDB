using System.Collections.Generic;
using IMDB.Services.Contracts;
using PingPong.Providers.Validation;
using System;

namespace IMDB.Services
{
	public class LoginSession : ILoginSession
	{
		private string loggedUser;
		private ICollection<string> loggedUserPermissions;
		public string LoggedUser { get => loggedUser;
			set
			{
				Validator.IfNull<ArgumentNullException>(value, "LoggedUser cannot be null!");
				this.loggedUser = value;
			}
		}
		public UserRoles LoggedUserRole { get; set; }
		public ICollection<string> LoggedUserPermissions {
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
