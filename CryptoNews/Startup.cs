using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Pomelo.EntityFrameworkCore.MySql.Storage;
//using Pomelo.EntityFrameworkCore.MySql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using CryptoNews.DAL.Repositories;
using CryptoNews.Core.IServices;
using CryptoNews.Services.Implement;

namespace CryptoNews
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
            services.AddControllersWithViews();
            services.AddSession();

            string connect = Configuration.GetConnectionString("MyConnect");
            /*ServerVersion version = ServerVersion.AutoDetect(connect);*/
            services.AddDbContextPool<CryptoNewsContext>(opt => opt
                .UseSqlServer(connect).UseLoggerFactory(LoggerFactory.Create(b => b
                .AddConsole()
                .AddFilter(lvl => lvl >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                    );

            services.AddTransient<IRepository<News>, NewsRepository>();
            services.AddTransient<IRepository<RssSource>, RssRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Role>, RoleRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();

            
            
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
