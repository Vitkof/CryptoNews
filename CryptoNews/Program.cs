using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging.Console;

namespace CryptoNews
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(LogEventLevel.Information)
                .WriteTo.File(@"LogFile.log", LogEventLevel.Warning, rollingInterval: RollingInterval.Day)
                .CreateBootstrapLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .SuppressStatusMessages(true)
                    .UseSetting("https_port", "443")
                    .UseSetting("detailedErrors", "true")
                    .UseStartup<Startup>();
                });
    }
}

