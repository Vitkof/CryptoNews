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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(CryptoNewsContext cont) : base(cont)
        {
        }

        public User ReadByFullName(string fullName)
        {
            throw new NotImplementedException();
        }
    }
}
