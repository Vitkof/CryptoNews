using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;

namespace CryptoNews.DAL.CQS.Queries.Comment
{
    public class GetCommentsByNewsIdQuery : IQuery<IEnumerable<CommentDto>>
    {
        public Guid NewsId { get; set; }
    }
}
