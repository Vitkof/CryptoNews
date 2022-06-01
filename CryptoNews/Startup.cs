using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using CryptoNews.DAL.Repositories;
using CryptoNews.Filters;
using CryptoNews.Core.IServices;
using CryptoNews.Services.Implement;
using CryptoNews.Policies;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using CryptoNews.Services.Implement.Mapper;
using CryptoNews.Services.Implement.NewsRating;

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
            #region Filters
            services.AddScoped<CustomExceptionFilterAttribute>();
            services.AddControllersWithViews().AddMvcOptions(options =>
            {
                options.MaxModelValidationErrors = 42;
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(new YandexFilterAttribute(8, 22));
                options.Filters.Add<LoggingFilter>();
            });
            services.AddScoped<NewsProviderFilter>();
            #endregion

            #region Session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            #endregion

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
            services.AddTransient<IRepository<Comment>, CommentRepository>();
            services.AddTransient<OnlinerParserService>();
            services.AddTransient<LentaParserService>();
            services.AddTransient<CointelegraphParserService>();
            services.AddTransient<BitcoinNewsParserService>();
            services.AddTransient<CryptoNinjasParserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IRssSourceService, RssSourceService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<INewsRatingService, NewsRatingService>();

            var mapperConfig = new MapperConfiguration(mc =>
            mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = new PathString("/Account/Login");
                    opt.AccessDeniedPath = new PathString("/Account/Login");
                });

            services.AddAuthorization(opt => opt.AddPolicy("18+", policy =>
                                    policy.Requirements.Add(new MinimalAgeReq(18))));
            services.AddSingleton<IAuthorizationHandler, MinimalAgeHandler>();
            
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

            app.UseSession();
            app.UseAuthentication();
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
