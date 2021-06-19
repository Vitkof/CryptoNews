using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement
{
    public class CommonParserActions
    {
        protected private static string DeleteA(string text)
        {
            var tagA = new Regex(@"(<a.*?>)|(</a>)", RegexOptions.Compiled);
            return tagA.Replace(text, "");
        }

        protected private static string DeleteP(string text)
        {
            var tagP = new Regex(@"(<p.*?>)|(</p>)", RegexOptions.Compiled);
            return tagP.Replace(text, " ");
        }

        protected private static string DeleteEM(string text)
        {
            var tagEM = new Regex(@"(<em>)|(</em>)", RegexOptions.Compiled);
            return tagEM.Replace(text, "\"");
        }

        protected private static string DeleteStrong(string text)
        {
            var tagStrong = new Regex(@"(<strong>)|(</strong>)", RegexOptions.Compiled);
            return tagStrong.Replace(text, "");
        }

        protected private static string DeleteBR(string text)
        {
            var tagBR = new Regex(@"<br>", RegexOptions.Compiled);
            return tagBR.Replace(text, "");
        }
    }
}
