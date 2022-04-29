using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity<Guid>
    {
        protected readonly CryptoNewsContext _db;
        protected readonly DbSet<T> _table;

        public BaseRepository(CryptoNewsContext context)
        {
            _db = context;
            _table = _db?.Set<T>();
        }

        
        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<T> entitiesRange)
        {
            await _table.AddRangeAsync(entitiesRange);
        }

        public T Read(Expression<Func<T, bool>> predic)
        {
            return _table.AsNoTracking().Where(predic).FirstOrDefault();
        }

        public IQueryable<T> ReadAll()
        {
            return _table;
        }

        public T ReadById(Guid id)
        {
            return _table.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<T> ReadMany(Expression<Func<T, bool>> predic, params Expression<Func<T, object>>[] includes)
        {
            var res = _table.Where(predic);
            if (includes.Any())
            {
                res = includes.Aggregate(res, (curr, include) 
                    => curr.Include(include));
            }
            return res;
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }


        public void Delete(Guid id)
        {
            T ent = ReadById(id);
            _table.Remove(ent);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            T ent = _table.AsNoTracking().Where(where).FirstOrDefault();
            _table.Remove(ent);
        }

        public void DeleteRange(IEnumerable<T> entitiesRange)
        {
            _table.RemoveRange(entitiesRange);
        }


        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
