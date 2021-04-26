using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.Services.Execution
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repos;

        public NewsService(INewsRepository repos)
        {
            _repos = repos;
        }

        public async Task AddNews(NewsDto nd)
        {
            var news = new News()
            {
                Id = nd.Id,
                Title = nd.Title,
                Description = nd.Description,
                Body = nd.Body,
                PubDate = nd.PubDate,
                Rating = nd.Rating,
                Url = nd.Url,
                RssSourceId = nd.RssSourceId
            };
            
            await _repos.Add(news);
        }

        public async Task AddRangeNews(IEnumerable<NewsDto> newsDto)
        {
            var range = newsDto.Select(nd => new News()
            {
                Id = nd.Id,
                Title = nd.Title,
                Description = nd.Description,
                Body = nd.Body,
                PubDate = nd.PubDate,
                Rating = nd.Rating,
                Url = nd.Url,
                RssSourceId = nd.RssSourceId
            }).ToList();
            
            await _repos.AddRange(range);

        }

        public Task<int> DeleteNews(NewsDto news)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditNews(NewsDto news)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsDto>> FindNews()
        {
            throw new NotImplementedException();
        }

        public NewsDto GetNewsById(Guid id)
        {
            var n = _repos.GetById(id);
            return new NewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Body = n.Body,
                PubDate = n.PubDate,
                Rating = n.Rating,
                Url = n.Url,
                RssSourceId = n.RssSourceId
            };
        }

        public async Task<IEnumerable<NewsDto>> GetNewsBySourceId(Guid? id)
        {
            var news = id.HasValue
                ? await _repos.GetAllNews()
                .Where(n => n.RssSourceId == id).ToListAsync()
                : await _repos.GetAllNews().ToListAsync();

            var newsDto = news.Select(n => new NewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Body = n.Body,
                PubDate = n.PubDate,
                Rating = n.Rating,
                Url = n.Url,
                RssSourceId = n.RssSourceId
            }).ToArray();
            return newsDto;
        }

        public async Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
