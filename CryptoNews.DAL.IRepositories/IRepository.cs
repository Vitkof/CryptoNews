using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoNews.DAL.IRepositories
{
    public interface IRepository<T>:IDisposable where T:class, IBaseEntity
    {
        Task Create(T entity);
        Task CreateRange(IEnumerable<T> entitiesRange);
        T Read(Expression<Func<T, bool>> where);
        T ReadById(Guid id);
        IQueryable<T> ReadAll();
        IQueryable<T> ReadMany(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task Update(T entity);
        Task Delete(Guid id);
        Task Delete(Expression<Func<T, bool>> where);
        Task DeleteRange(IEnumerable<T> entitiesRange);
    }
}
