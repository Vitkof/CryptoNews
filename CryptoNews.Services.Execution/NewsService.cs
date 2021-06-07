using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using CryptoNews.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.Services.Implement
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unit;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
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

            await _unit.News.Create(news);
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

            await _unit.News.CreateRange(range);
            await _unit.SaveChangesAsync();
        }

        public async Task<int> DeleteNews(NewsDto nd)
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

            await _unit.News.Delete(news.Id); 
            return 1;
        }

        public async Task<int> EditNews(NewsDto news)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            return _unit.News.ReadAll().Any(e => e.Id == id);
        }

        public Task<IEnumerable<NewsDto>> FindNews()
        {
            throw new NotImplementedException();
        }

        public NewsDto GetNewsById(Guid id)
        {
            var n = _unit.News.ReadById(id);
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
                ? await _unit.News.ReadMany(n => n.RssSourceId == id).ToListAsync()
                : await _unit.News.ReadMany(n => n != null).ToListAsync();

            var newsDtos = news.Select(n => new NewsDto()
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
            return newsDtos;
        }

        public NewsWithRssSourceNameDto GetNewsWithRssSourceNameById(Guid id)
        {
            var res = _unit.News.ReadMany(n => n.Id == id,
                                        n => n.RssSource)
                .Select(n => new NewsWithRssSourceNameDto()
                {
                    Id = n.Id,
                    Title = n.Title,
                    Description = n.Description,
                    Body = n.Body,
                    PubDate = n.PubDate,
                    Rating = n.Rating,
                    RssSourceId = n.RssSourceId
                });
            return (NewsWithRssSourceNameDto)res;
        }
    }
}
