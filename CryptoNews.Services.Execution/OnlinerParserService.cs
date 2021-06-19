using CryptoNews.Core.IServices;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CryptoNews.Services.Implement
{
    public class OnlinerParserService : CommonParserActions, IWebPageParser
    {
        public async Task<string> ParseBody(string url)
        {

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='news-text']");

            Regex paragraph = new Regex(@"<\s?p\s?>(.*?(<\s?/p\s?>))", RegexOptions.Compiled);
            MatchCollection matches = paragraph.Matches(node.InnerHtml);

            string body ="";
            foreach(var match in matches)
            {
                string pText = DeleteP(match.ToString());
                pText = DeleteEM(pText);
                pText = DeleteStrong(pText);
                body += pText;
            }

            return body;
        }

        public async Task<string> CleanDescription(string descript)
        {
            Regex reg = new Regex(@"\w(.*?[.!?])(.*?[.!?])", RegexOptions.Compiled);
            Match m = reg.Match(descript);
            return m.ToString();
        }
    }
}
