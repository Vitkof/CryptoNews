using CryptoNews.DAL.Entities;
using System;


namespace CryptoNews.DAL.IRepositories
{
    public interface INewsRepository : IRepository<News>
    {
        News ReadByTitle(string title);
    }
}