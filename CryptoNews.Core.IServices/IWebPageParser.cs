using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.IServices
{
    public interface IWebPageParser
    {
        Task<string> ParseBody(string url);
        Task<string> CleanDescription(string descript);
    }
}
