using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoNews.DAL.IRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment ReadByText(string text);
        IQueryable<Comment> GetOffensiveComments();
    }
}