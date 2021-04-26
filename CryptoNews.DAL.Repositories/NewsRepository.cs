using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly CryptoNewsContext _context;

        public NewsRepository(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task Add(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<News> newsRange)
        {
            await _context.News.AddRangeAsync(newsRange);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            News n = GetById(id);
            _context.News.Remove(n);
            await _context.SaveChangesAsync();
        }

        public News GetById(Guid id)
        {
            return _context.News.Find(id);
        }

        public IQueryable<News> GetAllNews()
        {
            return _context.News.Where(n => !String.IsNullOrWhiteSpace(n.Title));
        }

        public async Task Update(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
