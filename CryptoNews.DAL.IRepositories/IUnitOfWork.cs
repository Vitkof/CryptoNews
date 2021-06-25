using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> News { get; }
        IRepository<RssSource> RssSources { get; }
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Comment> Comments { get; }

        Task<int> SaveChangesAsync();
    }
}
