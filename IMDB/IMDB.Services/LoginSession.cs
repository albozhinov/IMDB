﻿using System.Collections.Generic;
using IMDB.Services.Contracts;
using System;
using IMDB.Services.Providers;
using IMDB.Data.Context;
using System.Linq;

namespace IMDB.Services
{
	public class LoginSession : ILoginSession
	{
		private int loggedUserID;
		private ICollection<string> loggedUserPermissions;
		public LoginSession(IMDBContext context)
		{
			LoggedUserPermissions = context.Permissions.Where(p => p.Rank <= (int)UserRoles.Guest).Select(p => p.Text).ToList();
			LoggedUserRole = UserRoles.Guest;
		}
		public int LoggedUserID
		{
			get => loggedUserID;
			set
			{
				Validator.IsNonNegative(value, "ID Cannot be negative!");
				loggedUserID = value;
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
