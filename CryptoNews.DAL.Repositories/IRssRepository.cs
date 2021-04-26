using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IRssRepository
    {
        Task Add(RssSource rss);
        Task AddRange(IEnumerable<RssSource> rssSources);
        Task Delete(Guid id);
        RssSource GetById(Guid id);
        IQueryable<RssSource> GetAllRssSources();
        Task Update(RssSource rss);
    }
}