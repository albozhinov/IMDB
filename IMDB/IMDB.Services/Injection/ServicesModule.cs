﻿using Autofac;
using IMDB.Services.Contracts;
using System.Reflection;

namespace IMDB.Services.Injection
{
	public sealed class ServicesModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.AsImplementedInterfaces();

			base.Load(builder);
		}

		private void RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterType<UserServices>().As<IUserServices>();
			builder.RegisterType<MovieServices>().As<IMovieServices>();
			builder.RegisterType<ReviewsService>().As<IReviewsServices>();
			builder.RegisterType<LoginSession>().As<ILoginSession>().SingleInstance();
		}
	}
}
