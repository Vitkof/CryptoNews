using CryptoNews.DAL.CQS.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;
using System;
using System.Linq;

namespace CryptoNews.WebAPI
{
    public static class Config
    {
        public static IServiceCollection AddAllQueryHandlers(
        this IServiceCollection services,
        ServiceLifetime withLifetime = ServiceLifetime.Transient)
        {
            return services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(classes =>
                    classes.AssignableTo(typeof(IQueryHandler<,>))
                        .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
                .AsSelfWithInterfaces()
                .WithLifetime(withLifetime)
            );
        }

        public static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface)
        {
            var handlers = Assembly.Load("CryptoNews.DAL.CQS").GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }
    }
}
