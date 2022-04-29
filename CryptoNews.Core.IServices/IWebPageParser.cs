using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.IServices
{
    public interface IWebPageParser
    {
        string ParseBody(string url);
        string CleanDescription(string descript);
    }
}
