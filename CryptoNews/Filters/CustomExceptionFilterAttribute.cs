using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;

namespace CryptoNews.Filters
{
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string action = context.ActionDescriptor.DisplayName;
            string message = context.Exception.Message;
            string stackTrace = context.Exception.StackTrace;
            HttpRequest request = context.HttpContext.Request;

            context.Result = new ViewResult()
            {
                ViewName = "CustomError"
            };

            Log.Error($"Error {message} || {action} || {stackTrace} || {request}");
            context.ExceptionHandled = true;
        }
    }
}
