using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.DAL.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(CryptoNewsContext cont) 
            : base(cont)
        { }

    }
}
