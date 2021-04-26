using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public interface IRssRepository : IRepository<RssSource>
    {
        RssSource ReadByName(string name);
    }
}