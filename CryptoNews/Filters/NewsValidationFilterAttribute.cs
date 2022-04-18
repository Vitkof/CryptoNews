using CryptoNews.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CryptoNews.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NewsValidationFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var newsObject = context.ActionArguments.SingleOrDefault(p => p.Value is NewsDto);
            if (newsObject.Value == null)
            {
                context.Result = new BadRequestObjectResult("News value cannot be null");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
