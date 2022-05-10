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
            strB.Append("<button id=\"prev\" click = \"prev\" >< Prev&nbsp;</button>");

            var firstA = $"<a href={pageUrl(1)} class=\"btn btn-default\"> 1 </a>";
            firstA = CheckPageNumberEquals(info, 1, firstA);
            strB.Append(firstA);

            int leftOffset = info.PageNumber - 2;  //mid
            int rightOffset = info.PageNumber + 2;

            if (info.PageNumber < 4) //begin
            {
                leftOffset = 2;
                rightOffset = 6;
            }

            if (info.PageNumber > info.CountPages-3) //end
            {
                leftOffset = info.CountPages - 5;
                rightOffset = info.CountPages - 1;
            }

            
            for (int i = leftOffset; i <= rightOffset; i++)
            {
                var str = $"<a href={pageUrl(i)} class=\"btn btn-default\"> {i} </a>";

                str = CheckPageNumberEquals(info, i, str);
                if (i == info.PageNumber)
                {
                    int ind = str.IndexOf("btn-default")+11;
                    str = str.Insert(ind, " selected primary");
                }

                strB.Append(str);
            }

            var lastA = $"<a href={pageUrl(info.CountPages)} class=\"btn btn-default\"> {info.CountPages} </a>";
            lastA = CheckPageNumberEquals(info, info.CountPages, lastA);
            strB.Append(lastA);

            strB.Append("<button id=\"next\" click = \"next\" >&nbsp;Next ></button>");
            return new HtmlString(strB.ToString());
        }

        private static string CheckPageNumberEquals(PageInfo pi, int num, string str)
        {
            if (pi.PageNumber == num)
            {
                int indx = str.IndexOf("btn-default") + 11;
                str = str.Insert(indx, " selected primary");
                int start = str.IndexOf("href");
                int end = str.IndexOf(' ', 3);
                str = str.Remove(start, end-start);

            }
            return str;
        }
    }
}
