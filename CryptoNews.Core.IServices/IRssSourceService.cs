using CryptoNews.Core.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoNews.Core.IServices
{
    public interface IRssSourceService
    {
        Task<IEnumerable<RssSourceDto>> GetAllRssSources();
        RssSourceDto GetRssSourceById(Guid id);
        //Task<IEnumerable<RssSourceDto>> GetRssSourceBySourceId(Guid? id);
        Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid id);

        Task AddRssSource(RssSourceDto rd);
        Task AddRangeRssSources(IEnumerable<RssSourceDto> rssDto);
        Task<int> EditRssSource(RssSourceDto rss);
        Task<int> DeleteRssSource(RssSourceDto rss);       
    }
}
