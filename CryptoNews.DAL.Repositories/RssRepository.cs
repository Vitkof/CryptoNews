using CryptoNews.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public class RssRepository : IRssRepository
    {
        private readonly CryptoNewsContext _context;

        public RssRepository(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task Add(RssSource rss)
        {
            await _context.RssSources.AddAsync(rss);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            RssSource rss = GetById(id);
            _context.RssSources.Remove(rss);
            await _context.SaveChangesAsync();
        }

        public RssSource GetById(Guid id)
        {
            return _context.RssSources.Find(id);
        }

        public IQueryable<RssSource> GetRssSources()
        {
            var l = _context.RssSources.Where(rss => !String.IsNullOrWhiteSpace(rss.Name));

            return l;
        }

        public async Task Update(RssSource rss)
        {
            _context.RssSources.Update(rss);  //проверка на существование записи
            await _context.SaveChangesAsync(); //обработка ошибок
        }
    }
}
