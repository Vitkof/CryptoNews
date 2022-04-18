using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CryptoNews.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LastVisitFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
