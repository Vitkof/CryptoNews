using Microsoft.AspNetCore.Razor.TagHelpers;


namespace CryptoNews.TagHelpers
{
    // <p bold></p> turn into <p><strong></strong></p>
    // <p><bold></bold></p>  BoldTagHelper willn't be called
    [HtmlTargetElement(Attributes = "bold")]
    public class BoldTagHelper : TagHelper
    {
        public override void Process(TagHelperContext cont, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("bold");
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");
            base.Process(cont, output);
        }
    }
}
