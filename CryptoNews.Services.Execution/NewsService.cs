using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
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

        private static News FromDtoToNews(NewsDto nd)
        {
            return new News()
            {
                Id = nd.Id,
                Title = nd.Title,
                Description = nd.Description,
                Body = nd.Body,
                PubDate = nd.PubDate,
                Rating = nd.Rating,
                Url = nd.Url,
                RssSourceId = nd.RssSourceId,               
            };
        }
        private static NewsDto FromNewsToDto(News n)
        {
            return new NewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
                Body = n.Body,
                PubDate = n.PubDate,
                Rating = n.Rating,
                Url = n.Url,
                RssSourceId = n.RssSourceId,
            };
        }

        public IEnumerable<NewsDto> AggregateNewsFromRssSources(IEnumerable<RssSourceDto> rssSources)
        {
            var listNewsDtos = new List<NewsDto>();

            foreach (var rssSrc in rssSources)
            {
                using var reader = XmlReader.Create(rssSrc.Url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                if (feed.Items.Any())
                {
                    var currentNewsUrls = _unit.News
                        .ReadAll()
                        .Select(news => news.Url)
                        .ToList();

                    listNewsDtos.AddRange(feed.Items
                        .Where(item => !currentNewsUrls.Any(url => url.Equals(item.Id)))
                        .Select(item => new NewsDto()
                        {
                            Id = Guid.NewGuid(),
                            Title = item.Title.Text,
                            Description = item.Summary.Text,
                            Body = "Заглушка",
                            PubDate = item.PublishDate.LocalDateTime,
                            Url = item.Id,
                            RssSourceId = rssSrc.Id
                        }));
                }
            }
            return listNewsDtos;
            /*var range = listNewsDtos.Select(nd => FromDtoToNews(nd));
            await _unit.News.CreateRange(range);
            await _unit.SaveChangesAsync();*/
        }

        public async Task AddNews(NewsDto nd)
        {
            var news = FromDtoToNews(nd);
            await _unit.News.Create(news);
            await _unit.SaveChangesAsync();
        }

        public async Task AddRangeNews(IEnumerable<NewsDto> newsDto)
        {
            var range = newsDto.Select(nd => FromDtoToNews(nd))
                .ToList();
            await _unit.News.CreateRange(range);
            await _unit.SaveChangesAsync();
        }

        public async Task<int> DeleteNews(NewsDto nd)
        {
            var news = FromDtoToNews(nd);
            await _unit.News.Delete(news.Id);
            return await _unit.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeNews(IEnumerable<NewsDto> nds)
        {
            return await Task.Run(async () =>
            {
                var range = nds.Select(nd => FromDtoToNews(nd));
                await _unit.News.DeleteRange(range);
                return await _unit.SaveChangesAsync();
            });            
        }

        public async Task<int> EditNews(NewsDto nd)
        {
            return await Task.Run(async () =>
            {
                var news = FromDtoToNews(nd);
                await _unit.News.Update(news);
                return await _unit.SaveChangesAsync();
            });
        }

        public bool Exist(Guid id)
        {
            return _unit.News.ReadAll().Any(e => e.Id == id);
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

            var newsDtos = news.Select(n => FromNewsToDto(n)).ToArray();
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
                    RssSourceId = (Guid)n.RssSourceId
                });
            return (NewsWithRssSourceNameDto)res;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNews()
        {
            var newsDtos = await _unit.News.ReadMany(n
               => !string.IsNullOrEmpty(n.Title))
                .Select(n => FromNewsToDto(n))
                .ToListAsync();

            return newsDtos;
        }
    }
}
