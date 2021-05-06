using CryptoNews.Core.DTO;
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
    public class RssRepository : BaseRepository<RssSource>, IRssRepository
    {

        public RssRepository(CryptoNewsContext cont) : base(cont)
        {
        }

        public RssSource ReadByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
