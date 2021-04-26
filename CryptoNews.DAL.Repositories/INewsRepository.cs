using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
        User ReadByTitle(string title);
    }
}