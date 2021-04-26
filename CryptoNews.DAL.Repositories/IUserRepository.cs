using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User ReadByFullName(string fullName);
        
    }
}
