using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IRepository<T> where T:IBaseEntity
    {
        Task Create(T entity);
        Task CreateRange(IEnumerable<T> entitiesRange);
        T Read(Expression<Func<T, bool>> where);
        T ReadById(Guid id);
        IQueryable<T> ReadAll();
        IQueryable<T> ReadMany(Expression<Func<T, bool>> where);
        Task Update(T entity);
        Task Delete(Guid id);
        Task Delete(Expression<Func<T, bool>> where);
        Task DeleteRange(IEnumerable<T> entitiesRange);
    }
}
