using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.DAL.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(CryptoNewsContext cont) 
            : base(cont)
        { }

        public News ReadByFullTitle(string title)
        {
            return _table.Where(n => n.Title.Equals(title)).SingleOrDefault();
        }

        public IQueryable<News> ReadBySubTitle(string titlePart)
        {
            return _table.Where(u => u.Title.Contains(titlePart));
        }

        public IQueryable<News> GetNotNullRss()
        {
            return _table.Where(n => n.RssSourceId != null); 
        }
    }
}
