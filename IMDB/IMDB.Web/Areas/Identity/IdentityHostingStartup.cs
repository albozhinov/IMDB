using System;
using IMDB.Data.Context;
using IMDB.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IMDB.Web.Areas.Identity.IdentityHostingStartup))]
namespace IMDB.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IMDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IMDBWebContextConnection")));

                services.AddDefaultIdentity<IMDBContext>()
                    .AddEntityFrameworkStores<IMDBContext>();
            });
        }
    }
}