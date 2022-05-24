using AutoMapper;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using CryptoNews.DAL.Repositories;
using CryptoNews.Services.Implement;
using CryptoNews.Services.Implement.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.DAL.CQS.QueryHandlers;
using System.Reflection;
using Hangfire;
using Hangfire.SqlServer;
using System.IO;

namespace CryptoNews.WebAPI
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

            #region Hangfire
            services.AddHangfire(conf => conf
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connect,
                new SqlServerStorageOptions() 
                { 
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(20),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(20),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            services.AddHangfireServer();
            #endregion

            #region AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddMediatR(typeof(GetRssByIdQueryHandler).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoNews.WebAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoNews.WebAPI v1"));

            app.UseHangfireDashboard();
            var newsService = provider.GetService(typeof(INewsService)) as INewsService;
            RecurringJob.AddOrUpdate(()=> newsService.AggregateNewsAsync(), "0,20,40 * * * *");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
