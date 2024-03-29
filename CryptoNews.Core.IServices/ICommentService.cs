﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface ICommentService
    {
        IEnumerable<CommentWithInfoDto> GetCommentsByNewsId(Guid newsId);
        CommentDto GetCommentById(Guid id);

        Task<CommentDto> GetCommentByIdAsync(Guid id);
        Task AddComment(CommentDto cd);
        Task<int> EditComment(CommentDto cd);
        Task<int> DeleteComment(CommentDto cd);
    }
}
