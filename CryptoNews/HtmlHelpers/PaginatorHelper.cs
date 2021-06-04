using CryptoNews.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;

namespace CryptoNews.HtmlHelpers
{
    public static class PaginatorHelper
    {
        public static HtmlString CreatePaginator(this IHtmlHelper hh,
                            PageInfo info, Func<int, string> pageUrl)
        {
            var strB = new StringBuilder();
            
            for(int i=1; i <= info.CountPages; i++)
            {
                var str = $"<a href={pageUrl(i)} class=\"btn btn-default\"> {i.ToString()} </a>";


                TagBuilder tagB = new TagBuilder("a");
                tagB.MergeAttribute("href", pageUrl(i));
                tagB.AddCssClass("btn btn-default");
                tagB.InnerHtml.Append(i.ToString());
                

                if (i == info.PageNumber)
                {
                    int ind = str.IndexOf("btn-default")+11;
                    str = str.Insert(ind, " selected primary");

                    tagB.AddCssClass("selected");
                    tagB.AddCssClass("primary");
                }

                //strB.Append(tagB);
                strB.Append(str);
            }
            return new HtmlString(strB.ToString());
        }
    }
}
