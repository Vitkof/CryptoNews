using CryptoNews.Core.IServices;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CryptoNews.Services.Implement
{
    public class LentaParserService : CommonParserActions, IWebPageParser
    {
        public async Task<string> ParseBody(string url)
        {

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//p");

            try
            {
                string body = "";
                byte i = 1;
                foreach (var match in nodes)
                {
                    string pText = DeleteP(match.InnerHtml);
                    pText = DeleteA(pText);
                    pText = DeleteBR(pText);
                    if (i == nodes.Count)      //Clean Author from EndNewsText
                    {
                        var tagSpan = new Regex(@"(<span class=).*?(</span>)");
                        pText = tagSpan.Replace(pText, "");
                    }
                    i++;
                    body += pText;
                }
               
                return body;
            }
            catch
            {
                return "Text-Zaglushka";
            }                        
        }

        public async Task<string> CleanDescription(string descript)
        {
            return descript;
        }
    }
}
