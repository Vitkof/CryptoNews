using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;

namespace CryptoNews.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class YandexFilterAttribute : Attribute, IResourceFilter
    {
        private readonly int _startHours;
        private readonly int _endHours;

        public YandexFilterAttribute(int startHours, int endHours)
        {
            _startHours = startHours;
            _endHours = endHours;
        }


        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var dateTime = DateTime.UtcNow;

            if (dateTime.Hour >= _startHours && dateTime.Hour <= _endHours)
            {
                context.HttpContext.Response.Headers.Add("resource_filter", DateTime.UtcNow.ToString("t"));
            }
            else
            {
                var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
                if (!userAgent.Contains("Yandex/"))
                {
                    context.Result = new ContentResult()
                    {
                        Content = "Russian browser not supported"
                    };
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
