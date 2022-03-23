using CryptoNews.Core.DTO;
using System;

namespace CryptoNews.DAL.CQS.Queries.Comment
{
    public class GetCommentByIdQuery : IQuery<CommentDto>
    {
        public Guid Id { get; set; }
    }
}
