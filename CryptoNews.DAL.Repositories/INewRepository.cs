using CryptoNews.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface INewRepository
    {
        Task Add(New news);
        Task Delete(Guid id);
        New GetById(Guid id);
        IQueryable<New> GetNews();
        Task Update(New news);
    }
}