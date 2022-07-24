using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.DAL.CQS.Queries.Rss;
using CryptoNews.DAL.CQS;
using System.Threading;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class RssSourceCQSService : IRssSourceService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public RssSourceCQSService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public async Task<IEnumerable<RssSourceDto>> GetAllRssSources()
        {
            var query = new GetAllRssQuery();
            return
                await _queryDispatcher
                .HandleAsync<GetAllRssQuery, IEnumerable<RssSourceDto>>(query, new CancellationToken());
        }

        public RssSourceDto GetRssSourceById(Guid id)
        {
            var query = new GetRssByIdQuery(id);
            return
                _queryDispatcher
                .Dispatch<GetRssByIdQuery, RssSourceDto>(query, new CancellationToken());
        }

        public async Task<RssSourceDto> GetRssSourceByNameUrl(string name, string url)
        {
            var query = new GetRssByNameUrlQuery(name, url);
            return
                await _queryDispatcher
                .HandleAsync<GetRssByNameUrlQuery, RssSourceDto>(query, new CancellationToken());
        }


        public Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddRssSource(RssSourceDto rd)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeRssSources(IEnumerable<RssSourceDto> rssDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditRssSource(RssSourceDto rss)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRssSource(RssSourceDto rss)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
