using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task CreateRange(IEnumerable<User> users);
        User Read(Expression<Func<User, bool>> where);
        User ReadById(Guid id);
        User ReadByFullName(string fullName);
        IQueryable<User> ReadAllUsers();
        IQueryable<User> ReadMany(Expression<Func<User, bool>> where);
        Task Update(User user);
        Task Delete(Guid id);
        Task Delete(Expression<Func<User, bool>> where);
    }
}
