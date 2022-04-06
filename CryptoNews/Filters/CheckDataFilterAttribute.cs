using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CryptoNews.Filters
{
    public class CheckDataFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly INewsService _newsService;

        public CheckDataFilterAttribute(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isContains = context.HttpContext.Request.QueryString.Value?.Contains("abc");
            if (isContains.GetValueOrDefault())
            {
                context.ActionArguments["hiddenId"] = 42;
                context.ActionArguments["news"] = await _newsService.GetNewsBySourceId(null);
            }

            await next();
        }
    }
}
