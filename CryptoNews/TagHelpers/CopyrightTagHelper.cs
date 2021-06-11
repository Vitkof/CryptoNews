using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace CryptoNews.TagHelpers
{
    // <p>© 2021 MyCompany</p>
    [HtmlTargetElement("copyright")]
    public class CopyrightTagHelper : TagHelper
    {        
        public override async Task<Task> ProcessAsync(TagHelperContext cont, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            string copy = $"<p>©{DateTime.Now.Year} {content.GetContent()}</p>";
            output.Content.SetHtmlContent(copy);
            return base.ProcessAsync(cont, output);
        }
    }
}
