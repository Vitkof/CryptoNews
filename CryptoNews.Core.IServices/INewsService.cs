using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> AggregateNewsFromRssSourcesAsync(IEnumerable<RssSourceDto> rssDtos);
        NewsDto GetNewsById(Guid id);
        Task<IEnumerable<NewsDto>> GetAllNews();
        Task<IEnumerable<NewsDto>> GetNewsBySourceId(Guid? id);
        NewsWithRssSourceNameDto GetNewsWithRssSourceNameById(Guid id);

        Task AddNews(NewsDto nd);
        Task AddRangeNews(IEnumerable<NewsDto> newsDto);
        Task<int> EditNews(NewsDto news);
        Task<int> DeleteNews(NewsDto news);
        Task<int> DeleteRangeNews(IEnumerable<NewsDto> nds);
        bool Exist(Guid id);
    }
}
