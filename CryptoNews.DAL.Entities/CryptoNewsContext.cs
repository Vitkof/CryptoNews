using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Entities
{
    public class CryptoNewsContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<RssSource> RssSources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public CryptoNewsContext(DbContextOptions<CryptoNewsContext> options) : base(options)
        { }
        
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql()
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Comment>().ToTable("Comments");
            model.Entity<New>().ToTable("News");
        }*/
    }
}
