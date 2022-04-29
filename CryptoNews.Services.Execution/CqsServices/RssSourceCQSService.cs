using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNews.DAL.Repositories;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CryptoNews.DAL.CQS.Queries.Rss;
using CryptoNews.DAL.CQS;
using CryptoNews.DAL.CQS.QueryHandlers.Rss;
using System.Threading;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class RssSourceCQSService : IRssSourceService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly QueryDispatcher _dispatcher;

        public RssSourceCQSService(IServiceProvider svc)
        {
            _serviceProvider = svc;
            _dispatcher = new QueryDispatcher(_serviceProvider);
        }

        public async Task<IEnumerable<RssSourceDto>> GetAllRssSources()
        {
            var query = new GetAllRssQuery();
            return
                await _dispatcher
                .HandleAsync<GetAllRssQuery, IEnumerable<RssSourceDto>>(query);
        }

        public RssSourceDto GetRssSourceById(Guid id)
        {
            var query = new GetRssByIdQuery(id);
            return
                _dispatcher
                .Dispatch<GetRssByIdQuery, RssSourceDto>(query);
        }

        public async Task<RssSourceDto> GetRssSourceByNameUrl(string name, string url)
        {
            var query = new GetRssByNameUrlQuery(name, url);
            return
                await _dispatcher
                .HandleAsync<GetRssByNameUrlQuery, RssSourceDto>(query);
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
