﻿using CryptoNews.Core.IServices;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CryptoNews.Services.Implement
{
    public class CointelegraphParserService : CommonParserActions, IWebPageParser
    {
        public string ParseBody(string url)
        {

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='post-content']");
            try
            {
                Regex paragraph = new Regex(@"(<p.*?>)(.*?(</p>))", RegexOptions.Compiled);
                MatchCollection matches = paragraph.Matches(node.InnerHtml);

                string body = "";
                foreach (var match in matches)
                {
                    string pText = DeleteP(match.ToString());
                    pText = DeleteA(pText);
                    pText = DeleteEM(pText);
                    pText = DeleteStrong(pText);
                    body += pText;
                }

                return body;
            }
            catch
            {
                return "Text-Zaglushka";
            }
        }

        public string CleanDescription(string descript)
        {
            Regex reg = new Regex(@"(?<=<p>)\s?\w.*?(?=</p>)", RegexOptions.Compiled);
            Match m = reg.Match(descript);
            return m.ToString();
        }
    }
}
