using CryptoNews.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public class NewRepository : INewRepository
    {
        private readonly CryptoNewsContext _context;

        public NewRepository(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task Add(New news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            New n = GetById(id);
            _context.News.Remove(n);
            await _context.SaveChangesAsync();
        }

        public New GetById(Guid id)
        {
            return _context.News.Find(id);
        }

        public IQueryable<New> GetNews()
        {
            var l = _context.News.Where(n => !String.IsNullOrWhiteSpace(n.Title));

            return l;
        }

        public async Task Update(New news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
        }
    }
}
