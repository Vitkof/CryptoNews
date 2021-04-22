using CryptoNews.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IRssRepository
    {
        Task Add(RssSource rss);
        Task Delete(Guid id);
        RssSource GetById(Guid id);
        IQueryable<RssSource> GetRssSources();
        Task Update(RssSource rss);
    }
}