using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;
using System.Threading;

namespace CryptoNews.Filters
{
    public class LocalizationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var language = (string)context.RouteData.Values["language"] ?? "en";
            var culture = (string)context.RouteData.Values["culture"] ?? "GB";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo($"{language}-{culture}");
        }
    }
}
