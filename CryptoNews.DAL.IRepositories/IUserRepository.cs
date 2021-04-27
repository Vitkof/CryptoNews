using CryptoNews.DAL.Entities;
using System;


namespace CryptoNews.DAL.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User ReadByFullName(string fullName);
        
    }
}
