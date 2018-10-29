//using System;
//using System.Collections.Generic;
//using IMDB.Console.Contracts;
//using IMDB.Services.Contracts;
//using IMDB.Services.Providers;

//namespace IMDB.Console.Commands
//{
//	public sealed class LoginCommand : ICommand
//	{
//		private const string FAILED_SYNTAX = "Wrong syntax of command";
//		private const string CMD_FORMAT = "register - <username> : <password>";
//		public LoginCommand(IUserServices userServices)
//		{
//			this.userServices = userServices;
//		}
//		public string Run(IList<string> parameters)
//		{
//			Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

//			if (parameters.Count != 2)
//				return $"{FAILED_SYNTAX}\nTry: {CMD_FORMAT}";
//			var userName = parameters[0];
//			var password = parameters[1];
//			userServices.Login(userName, password);

//			return $"Successfully logged in user {userName}";
//		}
//	}
//}
