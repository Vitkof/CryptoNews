using CryptoNews.Core.DTO;
using System.Collections.Generic;

namespace CryptoNews.Models.ViewModels
{
    public class NewsListWithPaginator
    {
        public IEnumerable<NewsDto> NewsPerPages { get; set; }
        public PageInfo PageInfo { get; set; }
        public bool IsAdmin { get; set; }
    }
}
