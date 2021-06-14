using CryptoNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace CryptoNews.TagHelpers
{
    public class PagTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelper;

        public PagTagHelper(IUrlHelperFactory urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public PageInfo PageInfo { get; set; }
        public string PageAction { get; set; }
        public string SourceId { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override Task ProcessAsync(TagHelperContext cont, 
            TagHelperOutput output)
        {
            
            var url = _urlHelper.GetUrlHelper(ViewContext);
            var res = new TagBuilder("ul");
            res.AddCssClass("pages-fastnav");

            for (int i=1; i<=PageInfo.CountPages; i++)
            {
                var tagA = new TagBuilder("a");                
                string anchor = GetAnchorInnerHtml(i, PageInfo);
                tagA.Attributes["href"] = url.Action(PageAction, 
                    new { sourceId = SourceId, pageNumber = i });                
                tagA.InnerHtml.Append(anchor);
                var li = new TagBuilder("li");
                li.InnerHtml.AppendHtml(tagA);
                res.InnerHtml.AppendHtml(li);
            }
            output.Content.AppendHtml(res);
            return base.ProcessAsync(cont, output);
        }

        public string GetAnchorInnerHtml(int i, PageInfo pi)
        {
            string anchor;
            if (pi.CountPages <= 1)
            {
                anchor = i.ToString();
            }
            else
            {
                if ((Math.Abs(i - pi.PageNumber) > 2) && i != 0 && i != pi.CountPages)
                    anchor = "...";
                else anchor = i.ToString();

            }
            return anchor;
        }
    }
}
