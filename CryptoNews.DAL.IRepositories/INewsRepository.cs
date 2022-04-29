using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoNews.DAL.IRepositories
{
    public interface INewsRepository : IRepository<News>
    {
        News ReadByFullTitle(string title);
        IQueryable<News> ReadBySubTitle(string titlePart);
        IQueryable<News> GetNotNullRss();
    }
}