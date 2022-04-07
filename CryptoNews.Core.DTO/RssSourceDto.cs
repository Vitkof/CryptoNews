using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Core.DTO
{
    public class RssSourceDto : Dto
    {
        public string Name { get; set; }
        [Url]
        public string Url { get; set; }
    }
}
