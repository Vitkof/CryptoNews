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
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CryptoNewsContext cont) 
            : base(cont)
        { }

        public Comment ReadByText(string text)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetOffensiveComments()
        {
            return _table.Where(c => !c.Active); 
        }
    }
}
