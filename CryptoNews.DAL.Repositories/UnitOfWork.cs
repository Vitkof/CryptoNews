using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CryptoNewsContext _cont;
        private readonly IRepository<News> _newsRepos;
        private readonly IRepository<RssSource> _rssRepos;
        private readonly IRepository<User> _userRepos;
        private readonly IRepository<Role> _roleRepos;

        public UnitOfWork(CryptoNewsContext cont, IRepository<News> newsR,
            IRepository<RssSource> rssR, IRepository<User> userR, 
            IRepository<Role> roleR)
        {
            _cont = cont;
            _newsRepos = newsR;
            _rssRepos = rssR;
            _userRepos = userR;
            _roleRepos = roleR;
        }

        public IRepository<News> News => _newsRepos;
        public IRepository<RssSource> RssSources => _rssRepos;
        public IRepository<User> Users => _userRepos;
        public IRepository<Role> Roles => _roleRepos;


        public async Task<int> SaveChangesAsync()
        {
            return await _cont.SaveChangesAsync();
        
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _cont.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            _cont?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
