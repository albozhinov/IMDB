using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IMDB.Data.Context;
using IMDB.Data.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using IMDB.Data.Repository;
using IMDB.Services;
using IMDB.Services.Contracts;
using System;
using IMDB.Web.Providers;

namespace IMDB.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<IMDBContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection")));

            services.BuildServiceProvider().GetService<IMDBContext>().Database.Migrate();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<IMDBContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));  
            services.AddScoped(typeof(IUserManager<>), typeof(UserManagerWrapper<>));


            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IReviewsServices, ReviewsService>();

			services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

            app.UseMvc(routes =>
			{
                routes.MapRoute(
                    name: "adminArea",
                    template: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

                routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
            });
		}
	}
}
