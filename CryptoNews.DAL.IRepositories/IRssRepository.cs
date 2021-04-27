using CryptoNews.DAL.Entities;
using System;


namespace CryptoNews.DAL.IRepositories
{
    public interface IRssRepository : IRepository<RssSource>
    {
        RssSource ReadByName(string name);
    }
}