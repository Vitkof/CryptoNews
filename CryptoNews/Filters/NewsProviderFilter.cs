using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CryptoNews.Filters
{
    public class NewsProviderFilter : IActionFilter
    {
        private readonly INewsService _newsService;

        public NewsProviderFilter(INewsService newsSvc)
        {
            _newsService = newsSvc;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("id", out object idValue))
            {
                throw new ArgumentException("id");
            }

            if (idValue == null)
            {
                context.Result = new NotFoundResult();
            }

            var id = (Guid)idValue;
            var result = _newsService.GetNewsById(id);

            if(result == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("news", result);
            }
        }
    }
}
