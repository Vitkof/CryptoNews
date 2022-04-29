using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoNews.DAL.IRepositories
{
    public interface IRepository<T>:IDisposable where T:class, IBaseEntity<Guid>
    {
        Task CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entitiesRange);
        T Read(Expression<Func<T, bool>> where);
        T ReadById(Guid id);
        IQueryable<T> ReadAll();
        IQueryable<T> ReadMany(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        void Update(T entity);
        void Delete(Guid id);
        void Delete(Expression<Func<T, bool>> where);
        void DeleteRange(IEnumerable<T> entitiesRange);
    }
}
