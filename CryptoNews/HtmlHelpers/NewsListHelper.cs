using CryptoNews.Core.DTO;
using CryptoNews.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Text;

namespace CryptoNews.HtmlHelpers
{
    public static class NewsListHelper
    {
        public static HtmlString CreateNewsList(this IHtmlHelper hh,
                            IEnumerable<NewsDto> news)
        {
            var strB = new StringBuilder();
            
            foreach(var nd in news)
            {
                strB.Append($"<div><h2>{nd.Title}</h2>");

                /* GENERATE HTML 
                 * <div>
                    <h2>@Model.Title</h2>
                    <div>
                        @Model.Body
                    </div>
                    <div>
                        <a asp-action="Details" asp-route-id="@Model.Id">Читать на аггрегаторе</a>
                    </div>
                    <div>
                        <a href="@Model.Url">Читать в источнике</a>
                    </div>
                </div>
                <hr/>
                 */
            }
            return new HtmlString(strB.ToString());
        }
    }
}
