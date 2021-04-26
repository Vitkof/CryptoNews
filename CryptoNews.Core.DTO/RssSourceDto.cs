using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Core.DTO
{
    public class RssSourceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Url]
        public string Url { get; set; }

    }
}
