using System;
using System.Collections.Generic;
using IMDB.Console.Contracts;
using IMDB.Services.Contracts;
using IMDB.Services.Providers;

namespace IMDB.Console.Commands
{
    public sealed class LogoutCommand : ICommand
    {
        private IUserServices userServices;
        public LogoutCommand(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        public string Run(IList<string> parameters)
        {
            Validator.IfNull<ArgumentNullException>(parameters, "Parameters cannot be null!");

            userServices.Logout();

            return $"Successfully logouted";
        }
    }
}
