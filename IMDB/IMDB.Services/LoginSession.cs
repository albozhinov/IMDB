using System.Collections.Generic;
using IMDB.Services.Contracts;
using System;
using IMDB.Services.Providers;
using IMDB.Data.Context;
using System.Linq;
using IMDB.Data.Models;
using IMDB.Data.Repository;

namespace IMDB.Services
{
	public class LoginSession : ILoginSession
	{
		private string loggedUserID;
		public LoginSession(IMDBContext context)
		{
			LoggedUserRole = UserRoles.Guest;
		}
		public string LoggedUserID
		{
			get => loggedUserID;
			set
			{
				Validator.IfIsNotPositive(Int32.Parse(value), "ID Cannot be negative or 0!");
				loggedUserID = value;
			}
		}

		public UserRoles LoggedUserRole { get; set; }
	}
}
