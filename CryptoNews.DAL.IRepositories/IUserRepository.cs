using CryptoNews.DAL.Entities;
using System;
using System.Linq;

namespace CryptoNews.DAL.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> ReadByFullName(string fullName);
    }
}
