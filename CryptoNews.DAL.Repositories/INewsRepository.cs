using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface INewsRepository
    {
        Task Add(News news);
        Task AddRange(IEnumerable<News> newsRange);
        Task Delete(Guid id);
        News GetById(Guid id);
        IQueryable<News> GetAllNews();
        Task Update(News news);
    }
}