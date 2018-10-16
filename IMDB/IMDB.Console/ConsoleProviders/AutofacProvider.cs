using Autofac;
using Autofac.Core.Registration;
using IMDB.Console.Contracts;
using System;

namespace IMDB.Console.ConsoleProviders
{
    public class AutofacProvider : IIOCProvider
    {
        private readonly ILifetimeScope scope;
        public AutofacProvider(ILifetimeScope scope)
        {
            this.scope = scope;
        }
        public T ResolveNamed<T>(string serviceName)
        {
            try
            {
                return scope.ResolveNamed<T>(serviceName.ToLower());
            }
            catch (ComponentNotRegisteredException)
            {
                throw new NotImplementedException("Service not found!");
            }
        }
    }
}
