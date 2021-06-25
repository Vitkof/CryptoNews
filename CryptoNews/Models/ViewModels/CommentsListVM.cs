using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;

namespace CryptoNews.Models.ViewModels
{
    public class CommentsListVM
    {
        public Guid NewsId { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
