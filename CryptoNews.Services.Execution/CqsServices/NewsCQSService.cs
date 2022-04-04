using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.News;
using MediatR;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class NewsCQSService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IQueryDispatcher _queryDispatcher;

        public NewsCQSService(IMediator mediator,
                              IQueryDispatcher dispatcher)
        {
            _mediator = mediator;
            _queryDispatcher = dispatcher;
        }


        #region Commands
        public async Task AddNews(NewsDto nd)
        {
            try
            {
                var cmd = new AddNewsCommand()
                {
                    Id = nd.Id,
                    Body = nd.Body,
                    Title = nd.Title,
                    Description = nd.Description,
                    PubDate = nd.PubDate,
                    Rating = nd.Rating,
                    Url = nd.Url,
                };

                var res = await _mediator.Send(cmd);
                Log.Information($"Added News: {nd.Url}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error AddNews: {ex.Message} {nd.Id}");
            }
        }

        public async Task AddRangeNews(IEnumerable<NewsDto> newsDto)
        {
            try
            {
                var num = await _mediator.Send(new AddRangeNewsCommand()
                { News = newsDto });
                Log.Information($"Added {num} News");
            }
            catch(Exception ex)
            {
                Log.Error($"Error AddRangeNews: {ex.Message}");
            }
        }

        public async Task<IEnumerable<NewsDto>> AggregateNewsFromRssSourcesAsync(IEnumerable<RssSourceDto> rssDtos)
        {
            try
            {
                var news = new ConcurrentBag<NewsDto>();
                var urls = await GetAllUrlsFromNews();
                Parallel.ForEach(rssDtos, (rssSource) =>
                {
                    using (var reader = XmlReader.Create(rssSource.Url))
                    {
                        var feed = SyndicationFeed.Load(reader);

                        reader.Close();
                        if (feed.Items.Any())
                        {
                            foreach (var syndicationItem in feed.Items
                                .Where(item => !urls.Any(url => url.Equals(item.Id))))
                            {
                                var newsDto = new NewsDto()
                                {
                                    Id = Guid.NewGuid(),
                                    RssSourceId = rssSource.Id,
                                    Url = syndicationItem.Id,
                                    Title = syndicationItem.Title.Text,
                                    Description = syndicationItem.Summary.Text //Parser(?)
                                };
                                news.Add(newsDto);
                            }
                        }
                    }
                });

                return news;
            }
            catch(Exception ex)
            {
                Log.Error($"Aggregation was failed{ex.Message}");
                return null;
            }   
        }

        public async Task<int> DeleteNews(NewsDto news)
        {
            try
            {
                var cmd = new DeleteNewsCommand() 
                { Id = news.Id };
                await _mediator.Send(cmd);
                return 1;
            }
            catch(Exception ex)
            {
                Log.Error($"DeleteNews Error: {ex.Message}");
                return 0;
            }
        }

        public Task<int> DeleteRangeNews(IEnumerable<NewsDto> nds)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditNews(NewsDto news)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Queries
        public async Task<IEnumerable<NewsDto>> GetAllNews()
        {
            try
            {
                var query = new GetAllNewsQuery();
                return
                    await _queryDispatcher
                    .HandleAsync<GetAllNewsQuery, IEnumerable<NewsDto>>(query, new CancellationToken());
            }
            catch (Exception ex)
            {
                Log.Error($"GetAllNews Error: {ex.Message}");
                return null;
            }
        }

        public NewsDto GetNewsById(Guid id)
        {
            try
            {
                var query = new GetNewsByIdQuery(id);
                return
                    _queryDispatcher
                    .Dispatch<GetNewsByIdQuery, NewsDto>(query, new CancellationToken());
            }
            catch(Exception ex)
            {
                Log.Error($"GetNewsById Error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<NewsDto>> GetNewsBySourceId(Guid? id)
        {
            try
            {
                var query = new GetNewsByRssIdQuery((Guid)id);
                return
                    await _queryDispatcher
                    .HandleAsync<GetNewsByRssIdQuery, IEnumerable<NewsDto>>(query, new CancellationToken());
            }
            catch (Exception ex)
            {
                Log.Error($"GetNewsBySourceId Error: {ex.Message}");
                return null;
            }
        }

        public NewsWithRssSourceNameDto GetNewsWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetAllUrlsFromNews()
        {
            try
            {
                var query = new GetAllUrlsFromNewsQuery();
                return
                    await _queryDispatcher
                    .HandleAsync<GetAllUrlsFromNewsQuery, IEnumerable<string>>(query, new CancellationToken());
            }
            catch (Exception e)
            {
                Log.Error($"GetAllNews Exception {e.Message}");
                return null;
            }
        }
        #endregion
    }
}
