using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNews.DAL.Repositories;
using CryptoNews.DAL.Entities;

namespace CryptoNews.Services.Execution
{
    public class RssSourceService : IRssSourceService
    {
        private readonly IRssRepository _repos;

        public RssSourceService(IRssRepository repos)
        {
            _repos = repos;
        }

        

        public async Task AddRssSource(RssSourceDto rd)
        {
            var rss = new RssSource()
            {
                Id = rd.Id,
                Name = rd.Name,
                Url = rd.Url
            };
            await _repos.Add(rss);
        }

        public async Task AddRangeRssSources(IEnumerable<RssSourceDto> rssDto)
        {
            var range = rssDto.Select(rd => new RssSource()
            {
                Id = rd.Id,
                Name = rd.Name,
                Url = rd.Url
            }).ToList();
            await _repos.AddRange(range);
        }

        public Task<int> DeleteRssSource(RssSourceDto rss)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditRssSource(RssSourceDto rss)
        {
            throw new NotImplementedException();
        }

        public Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RssSourceDto> GetRssSourceById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
