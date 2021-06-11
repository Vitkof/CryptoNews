using Microsoft.AspNetCore.Razor.TagHelpers;


namespace CryptoNews.TagHelpers
{
    [HtmlTargetElement("email", TagStructure = TagStructure.NormalOrSelfClosing)]  // <email ... />
    public class EmailTagHelper : TagHelper
    {
        private const string _domain = "example.com";

        public string MailTo { get; set; }

        public override void Process(TagHelperContext cont, TagHelperOutput output)
        {
            output.TagName ="a";
            string address = $"{MailTo}@{_domain}";
            output.Attributes.Add("href", $"mailto:{address}");
            output.Content.SetContent(address);
            base.Process(cont, output);
        }
    }
}
