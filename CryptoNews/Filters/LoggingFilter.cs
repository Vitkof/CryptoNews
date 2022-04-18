using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace CryptoNews.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LoggingFilter()
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            _logger = loggerFactory.CreateLogger("LoggingFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"{context.ActionDescriptor.DisplayName} executed - {DateTime.Now}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"{context.ActionDescriptor.DisplayName} executing");
        }
    }
}
