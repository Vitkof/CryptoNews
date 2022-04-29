using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IRepository<Comment> _commentRepos;

        public UnitOfWork(CryptoNewsContext cont, IRepository<News> newsR,
            IRepository<RssSource> rssR, IRepository<User> userR, 
            IRepository<Role> roleR, IRepository<Comment> commentR)
        {
            _cont = cont;
            _newsRepos = newsR;
            _rssRepos = rssR;
            _userRepos = userR;
            _roleRepos = roleR;
            _commentRepos = commentR;
        }

        public IRepository<News> News => _newsRepos;
        public IRepository<RssSource> RssSources => _rssRepos;
        public IRepository<User> Users => _userRepos;
        public IRepository<Role> Roles => _roleRepos;
        public IRepository<Comment> Comments => _commentRepos;


        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _cont.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return await _cont.SaveChangesAsync().ConfigureAwait(false);
            }
            catch(DataException ex)
            {
                Log.Error($"SaveDB Error: {ex}");
                Console.WriteLine(ex);
                return 0;
            }
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
